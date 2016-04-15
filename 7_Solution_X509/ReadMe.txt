#######################################
#                                     #
#   X.509 Certificate read & verify   #
#                                     #
#######################################
by Thomas Schmiedecker


Solution Parts
******************************
1)	Head.cs


Build Information
******************************
Target-Framework:
.NET Framework 4.5 (compatible with Apple)

Additional References:
System.Security.Cryptography.X509Certificates;


Program structure
******************************
x)	Program starts and imports VeriSign certificate
x)	Path, filename and extension is hardcoded
x)	After the import, the certificate will be verified


Setup and program start
******************************
1) Unzip package in directory/folder of your choice
2) Navigate to "...\7_Solution_X509\X509\bin\Release"
4) Start application "X509.exe"