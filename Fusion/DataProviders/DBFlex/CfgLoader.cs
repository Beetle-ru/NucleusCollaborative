using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;
using System.IO;

namespace DBFlex
{
    public class CfgLoader {
        private string m_mainDir;
        
        private const char Separator = ';';

        public CfgLoader(string mainDir) {
            m_mainDir = mainDir;
        }

        public Result ReadCfg(FlexEvent flx)
        {
            var res = new Result();

            #region Init and verify

            var subDir = "";
            var command = "";
            
            if (flx.Arguments.ContainsKey(Program.ArgEventName)) {
                subDir = (string)flx.Arguments[Program.ArgEventName];
                if (String.IsNullOrWhiteSpace(subDir)) {
                    res.ErrorCode = Result.Es.S_ERROR;
                    res.ErrorStr += Program.ArgEventName + " is not contains value\n";
                }
            }
            else {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("Argument {0} is not found\n", Program.ArgEventName);
            }

            if (flx.Arguments.ContainsKey(Program.ArgCommandName))
            {
                command = (string)flx.Arguments[Program.ArgCommandName];
                if (String.IsNullOrWhiteSpace(command))
                {
                    res.ErrorCode = Result.Es.S_ERROR;
                    res.ErrorStr += Program.ArgCommandName + " is not contains value\n";
                }
            }
            else
            {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("Argument {0} is not found\n", Program.ArgCommandName);
            }

            if (res.ErrorCode == Result.Es.S_ERROR) return res;

            var currentDir = String.Format("{0}\\{1}", m_mainDir, subDir);

            if (!Directory.Exists(currentDir)) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("Directory {0} is not found\n", currentDir);
                return res;
            }

            var indexFile = String.Format("{0}\\{1}", currentDir, Program.IndexFileName);

            if (!File.Exists(indexFile))
            {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("Index file {0} is not found\n", indexFile);
                return res;
            }
            #endregion

            #region Load index file

            string[] indexStrings;

            try {
                indexStrings = File.ReadAllLines(indexFile);
            }
            catch (Exception e ) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }
            
            if (!indexStrings.Any()) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("Index file {0} is empty\n", indexFile);
                return res;
            }

            var sqlFileName = "";

            for (int i = 0; i < indexStrings.Count(); i++) {
                var kv = indexStrings[i].Split(Separator);
                if (kv.Length >= 2) {
                    var k = kv[0];
                    var v = kv[1];
                    if (k == (string) flx.Arguments[Program.ArgCommandName]) {
                        sqlFileName = String.Format("{0}\\{1}", currentDir, v);
                    }
                    else {
                        if (!File.Exists(String.Format("{0}\\{1}", currentDir, v))) {
                            res.ErrorCode = Result.Es.S_WARN;
                            res.ErrorStr +=
                                String.Format(
                                    "Index file {0} included the \"{1}\" SQL file, but this SQL file can't be found\n",
                                    indexFile, v);
                        }
                    }
                }
                else {
                    res.ErrorCode = Result.Es.S_WARN;
                    res.ErrorStr += String.Format("Index file {2} have bad string:\n{1}. \"{0}\"\n", indexStrings[i],
                                                  i + 1, indexFile);
                }
            }

            if (String.IsNullOrWhiteSpace(sqlFileName)) {
                    res.ErrorCode = Result.Es.S_ERROR;
                    res.ErrorStr += String.Format("Command {0} or associated file not found\n", (string)flx.Arguments[Program.ArgCommandName]);
                    return res;
                }

           if (!File.Exists(sqlFileName)) {
                    res.ErrorCode = Result.Es.S_ERROR;
                    res.ErrorStr += String.Format("SQL file \"{0}\" do not be found\n", sqlFileName);
                    return res;
                }


            #endregion

            #region Load SQL file

            var sqlFileData = ""; 

            try {
                sqlFileData = File.ReadAllText(sqlFileName);
            }
            catch (Exception e) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }

            //sqlFileData = sqlFileData.Replace('\n', ' ');

            if (String.IsNullOrWhiteSpace(sqlFileData)) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("SQL file \"{0}\" is empty\n", sqlFileData);
                return res;
            }

            #endregion

            #region template

            var patterns = new Dictionary<string, string>();

            foreach (var argument in flx.Arguments) {
                if ((argument.Key != Program.ArgEventName) && (argument.Key != Program.ArgCommandName)) {
                    patterns.Add(argument.Key, argument.Value.ToString());
                }
            }

            var templateRes = ApplyTemplate(sqlFileData, patterns);

            res.ErrorCode = templateRes.ErrorCode;
            res.ErrorStr += templateRes.ErrorStr;
            res.SQLStr = templateRes.SQLStr;

            #endregion

            //res.SQLStr = sqlFileData;

            return res;
        }

        private Result ApplyTemplate(string sqlStr, Dictionary<string, string> patterns)
        {
            const char beginEscpe = '<';
            const char endEscpe = '>';

            var res = new Result();

            var sqlArray = sqlStr.ToCharArray();
            var sqlLength = sqlArray.Count();
            sqlStr = "";

            var substr = "";
            var beginSubstr = false;

            for (int i = 0; i < sqlLength; i++) {
                if ((sqlArray[i] == beginEscpe) || beginSubstr)
                {
                    beginSubstr = true;
                }
                else {
                    sqlStr += sqlArray[i];
                }
                
                if (beginSubstr) {
                    if (sqlArray[i] == endEscpe){
                        if (patterns.ContainsKey(substr)) {
                            sqlStr += patterns[substr];
                            substr = "";
                            beginSubstr = false;
                        }
                        else {
                            sqlStr += String.Format("{0}{1}{2}", beginEscpe, substr, endEscpe);
                            res.ErrorCode = Result.Es.S_ERROR;
                            res.ErrorStr += String.Format("Parameter \"{1}{0}{2}\" is not specified in the request FlexEvent", substr, beginEscpe, endEscpe);
                        }
                        //sqlStr += sqlArray[i]; // not remoove end symbol
                    }
                    else {
                        if (sqlArray[i] != beginEscpe) substr += sqlArray[i]; // remove start symbol
                    }
                }
            }
            res.SQLStr = sqlStr;
            return res;
        }

       
        public class Result {
            public Result() {
                ErrorStr = "";
                SQLStr = "";
                ErrorCode = Es.S_OK;
            }
            public enum Es {
                S_OK,
                S_WARN,
                S_ERROR
            }
            public string ErrorStr;
            public string SQLStr;
            public Es ErrorCode;
        }
    }
}
