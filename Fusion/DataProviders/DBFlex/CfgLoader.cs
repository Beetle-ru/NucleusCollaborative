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
        private const string ArgEventName = "@EventName";
        private const string ArgCommand = "@Command";
        private const string IndexFileName = "Index";
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
            
            if (flx.Arguments.ContainsKey(ArgEventName)) {
                subDir = (string)flx.Arguments[ArgEventName];
                if (String.IsNullOrWhiteSpace(subDir)) {
                    res.ErrorCode = Result.Es.S_Error;
                    res.ErrorStr += ArgEventName + " is not contains value\n";
                }
            }
            else {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("Argument {0} is not found\n", ArgEventName);
            }

            if (flx.Arguments.ContainsKey(ArgCommand))
            {
                command = (string)flx.Arguments[ArgCommand];
                if (String.IsNullOrWhiteSpace(command))
                {
                    res.ErrorCode = Result.Es.S_Error;
                    res.ErrorStr += ArgCommand + " is not contains value\n";
                }
            }
            else
            {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("Argument {0} is not found\n", ArgCommand);
            }

            if (res.ErrorCode == Result.Es.S_Error) return res;

            var currentDir = String.Format("{0}\\{1}", m_mainDir, subDir);

            if (!Directory.Exists(currentDir)) {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("Directory {0} is not found\n", currentDir);
                return res;
            }

            var indexFile = String.Format("{0}\\{1}", currentDir, IndexFileName);

            if (!File.Exists(indexFile))
            {
                res.ErrorCode = Result.Es.S_Error;
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
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }
            
            if (!indexStrings.Any()) {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("Index file {0} is empty\n", indexFile);
                return res;
            }

            var sqlFileName = "";

            for (int i = 0; i < indexStrings.Count(); i++)
            {
                var kv = indexStrings[i].Split(Separator);
                if (kv.Length >= 2)
                {
                    var k = kv[0];
                    var v = kv[1];
                    if (k == (string)flx.Arguments[ArgCommand]) {
                        sqlFileName = String.Format("{0}\\{1}", currentDir, v);
                    }
                    else {
                        if (!File.Exists(String.Format("{0}\\{1}", currentDir, v))) {
                            res.ErrorCode = Result.Es.S_Warn;
                            res.ErrorStr += String.Format("Index file {0} included the \"{1}\" SQL file, but this SQL file can't be found\n", indexFile, v);
                        }
                    }
                }
                else {
                    res.ErrorCode = Result.Es.S_Warn;
                    res.ErrorStr += String.Format("Index file {2} have bad string:\n{1}. \"{0}\"\n", indexStrings[i], i + 1, indexFile);
                }

                if (String.IsNullOrWhiteSpace(sqlFileName)) {
                    res.ErrorCode = Result.Es.S_Error;
                    res.ErrorStr += String.Format("Command {0} or associated file not found\n", (string)flx.Arguments[ArgCommand]);
                    return res;
                }

                if (!File.Exists(sqlFileName))
                {
                    res.ErrorCode = Result.Es.S_Error;
                    res.ErrorStr += String.Format("SQL file \"{0}\" do not be found\n", sqlFileName);
                    return res;
                }
            }

            #endregion

            #region Load SQL file

            var sqlFileData = ""; 

            try {
                sqlFileData = File.ReadAllText(sqlFileName);
            }
            catch (Exception e) {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }

            //sqlFileData = sqlFileData.Replace('\n', ' ');

            if (String.IsNullOrWhiteSpace(sqlFileData)) {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("SQL file \"{0}\" is empty\n", sqlFileData);
                return res;
            }

            #endregion

            res.SQLStr = sqlFileData;

            return res;
        }

        private string ApplyTemplate(string sqlStr, Dictionary<string, string> pattern) {
            const char startEscpe = ':';
            const char endEscpe = ' ';

            var sqlArray = sqlStr.ToCharArray();
            var sqlLength = sqlArray.Count();
            sqlStr = "";

            var substr = "";

            for (int i = 0; i < sqlLength; i++) {
                if (sqlArray[i] == startEscpe) {
                    if (sqlArray[i] == endEscpe) // еще может быть конец строки !!!
                    {
                        if (pattern.ContainsKey(substr)) {
                            sqlStr += pattern[substr];
                        }
                        sqlStr += sqlArray[i]; // not remoove end symbol
                    }
                    else {
                        if (sqlArray[i] != startEscpe) substr += sqlArray[i]; // remove start symbol
                    }
                }
                else {
                    sqlStr += sqlArray[i];
                }
            }

            return sqlStr;
        }

       
        public class Result {
            public Result() {
                ErrorStr = "";
                SQLStr = "";
                ErrorCode = Es.S_Ok;
            }
            public enum Es {
                S_Ok,
                S_Warn,
                S_Error
            }
            public string ErrorStr;
            public string SQLStr;
            public Es ErrorCode;
        }
    }
}
