################################
#                              #
#   EC Scalar Multiplication   #
#                              #
################################
by Thomas Schmiedecker

Program start
******************************
1)	Navigate to folder 5_Solution_ECScalarMultiplcation\ECScalarMultiplication\bin\Release
2)	Doubleclick the application RSA.exe

Solution Parts
******************************
1)	ScalarMultiplication.cs
2)	ExtendedEuclidianAlog.cs
3)	CurveMethods.cs
4)	EllipticCurve.cs
5)	ECPoint.cs


Build Information
******************************
Target-Framework:
.NET Framework 4.5 (compatible with Apple)

Additional References:
Sytem.Numerics


Program structure
******************************
x)	Program starts and instance Nist-P-224 according 
	"RECOMMENDED ELLIPTIC CURVES FOR FEDERAL GOVERNMENT USE (Juli '99
x)	In the next step, the User can enter a scalar


Setup and program start
******************************
1) Unzip package in directory/folder of your choice
2) Server has to be started first
3) Navigate to "...\3_Solution_ClientServer\Server\bin\Release"
4) Start application "Server.exe"
5) Navigate to "...\3_Solution_ClientServer\Client\bin\Release"
6) Start application "Client.exe"
7) Chat is ready to go