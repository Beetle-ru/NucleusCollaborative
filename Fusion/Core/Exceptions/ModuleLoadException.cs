﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Exceptions {
    internal class ModuleLoadException : Exception {
        private string _Message = string.Empty;

        public ModuleLoadException(string message) {
            _Message = message;
        }

        public override string ToString() {
            return _Message;
        }
    }
}