using System.Diagnostics;

namespace ActivityUtilities
{
    public class Utilities
    {
        public static bool IsBannedProcess(Process p)
        {
            switch (p.ProcessName.ToLower())
            {
                case "svchost":
                    return true;
                case "conhost":
                    return true;
                case "symsrvhost":
                    return true;
                case "sihost.exe":
                    return true;
                case "taskhostw":
                    return true;
                case "textinputhost":
                    return true;
                case "applicationframehost":
                    return true;
                case "runtimebroker":
                    return true;
                case "backgroundtaskhost":
                    return true;
                case "dllhost":
                    return true;
                case "crashpad_handler":
                    return true;
                case "sdmicmute":
                    return true;
                case "idle":
                    return true;
                case "system":
                    return true;
                case "secure system":
                    return true;
                case "registry":
                    return true;
                case "smss":
                    return true;
                case "csrss":
                    return true;
                case "wininit":
                    return true;
                case "services":
                    return true;
                case "lsaiso":
                    return true;
                case "lsass":
                    return true;
                case "amdfendrsr":
                    return true;
                case "atiesrxx":
                    return true;
                case "memory compression":
                    return true;
                case "audiodg":
                    return true;
                case "spoolsv":
                    return true;
                case "wlanext":
                    return true;
                case "vmcompute":
                    return true;
                case "ijplmsvc":
                    return true;
                case "sqlwriter":
                    return true;
                case "tmshinstall":
                    return true;
                case "tminstall":
                    return true;
                case "jhi_service":
                    return true;
                case "dashost":
                    return true;
                case "msmpeng":
                    return true;
                case "mpdefendercoreservice":
                    return true;
                case "fontdrvhost":
                    return true;
                case "saservice":
                    return true;
                case "aggregatorhost":
                    return true;
                case "nissrv":
                    return true;
                case "vmmemcmzygote":
                    return true;
                case "searchindexer":
                    return true;
                case "securityhealthservice":
                    return true;
                case "atieclxx":
                    return true;
                case "dwm":
                    return true;
                case "winlogon":
                    return true;
                case "wudfhost":
                    return true;
                case "wmiprvse":
                    return true;
                case "intelcphdcpsvc":
                    return true;
                case "ctfmon":
                    return true;
                case "rpmdaemon":
                    return true;
                case "graphicscardengine":
                    return true;
                case "amdrsserv":
                    return true;
                case "amdow":
                    return true;
                case "searchprotocolhost":
                    return true;
                case "searchfilterhost":
                    return true;
                case "standardcollector.service":
                    return true;
                case "sihost":
                    return true;
                case "systemsettings":
                    return true;
                case "lockapp":
                    return true;
                default:
                    return false;
            }
        }
    }
}
