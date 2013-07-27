using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser {
    internal class PatternBank {

        private static List<string> patterns = new List<string> { "Tn,Tq,Tp", "Tn,TP", "Tf,To", "Tp,Tw", "Tp, Tn" };

        internal static bool completesTagPattern(string buffer, string tag) {
            string[] patterns = matchCurrentBuffer(buffer);
            foreach (string p in patterns) {
                if (completesPattern(p, buffer, tag)) {
                    return true;
                }
            }
            return false;
        }

        internal static bool continuesTagPattern(string buffer, string tag) {
            string[] patterns = matchCurrentBuffer(buffer);
            foreach (string p in patterns) {
                if (continuesPattern(p, buffer, tag)) {
                    return true;
                }
            }
            return false;
        }

        private static string[] matchCurrentBuffer(string buffer) {
            List<string> matches = new List<string>();
            foreach (string p in patterns) {
                if (p.Contains(buffer))
                    matches.Add(p);
            }
            return matches.ToArray();
        }

        private static bool completesPattern(string pattern, string buffer, string tag) {
            if (pattern == buffer + "," + tag) {
                return true;
            }
            return false;
        }

        private static bool continuesPattern(string pattern, string buffer, string tag) {
            if (pattern.Contains(buffer + "," + tag)) {
                return true;
            }
            return false;
        }
    }
}
