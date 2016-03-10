#########################
#						#
#	CLientServer Setup	#
#						#
#########################


Solution Parts
******************************
1)	ClientClass.cs
2)	ServerClass.cs

Build Information
******************************
Target-Framework:
.NET Framework 4.5 (compatible with Apple)

Program structure
******************************
x) Program starts and user has possibility to choose arbitrary text
x) As it is a synchronous chat-system, the communication runs as follows:	
	x) Clients starts the conversation by sending a message
	x) Server receives message and can answer
	x) After the client received the server's message, he is able to answer
x) To end the conversation server or client have to send "exit"

Setup and program start
******************************
1) Unzip package in directory/folder of your choice
2) Server has to be started first
3) Navigate to "...\3_Solution_ClientServer\Server\bin\Release"
4) Start application "Server.exe"
5) 3) Navigate to "...\3_Solution_ClientServer\Client\bin\Release"
6) Start application "Client.exe"
7) Chat is ready to go