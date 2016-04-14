#################
#               #
#   RSA Setup   #
#               #
#################
by Thomas Schmiedecker

Program start
******************************
1)	Navigate to folder 1_Solution_RSA\RSA\bin\Release
2)	Doubleclick the application RSA.exe

Solution Parts
******************************
1)	RSA_Encrypt_Decrypt.cs
2)	ExtendedEuclidianAlog.cs
3)	MRT.cs


Build Information
******************************
Target-Framework:
.NET Framework 4.5 (compatible with Apple)

If References are missing, add the following:
	1)	Extended_Euclidian
	2)	MillerRabinTest

How to add references with Visual Studio:
	1)	Right click on project name
	2)	Add Reference...
	3)	Navigate to "Projects"
	4)	Tick Extended_Euclid and MillerRabinTest

Import Euclid and MillerRabin classes to RSA with the "using"-statement


Program structure
******************************
x) Program starts and generates private and public keys
x) User will be asked for input
x) Encryption and Decryption values will be displayed
x) Decrypted string will be displayed to control the algorithm






