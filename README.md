# PC-Configurator-Console-Application
C# Console app as part of my application.

# Project Specification
A local computer store wants to build a brand-new website for selling computers, allowing the customers to choose individual parts. From the store owner’s previous experience, customers often buy parts that are incompatible, which leads to the store covering return shipping expenses and reduces the profit of the store.
In order to minimize their expenses, the store wants to build a computer configurator that will not allow the customer to buy incompatible parts. 
You are tasked to create a PC configurator that suggests valid configurations or validates the compatibility of the customer’s choice of components.
The store sells 3 types of components:	
1.	CPUс
2.	DDR Memory
3.	Motherboards
Each component has properties that define its compatibility with other components:
1.	CPUs – CPU compatibility is defined by the Socket of the CPU and the Memory type Supported. 
•	CPUs are compatible only with Motherboards with the same socket.
•	CPUs are compatible only with the type of memory they support.
2.	DDR Memory 
•	Memory is compatible only with CPUs that support it.
3.	Motherboards
•	Motherboards are compatible with CPUs with the same socket.
4.	In the real world some motherboards and CPUs support multiple memory types. For the purposes of this task those cases should not be taken into consideration and only the limitations listed above should be used. 

Task requirements:
The list of items in the inventory is stored in a file named “pc-store-inventory.json”.
Implement a console application that loads the inventory, accepts a CPU, Memory and Motherboard part numbers and generates configurations with the selected parts. 
•	When the user provides a full list of components the provided configuration is validated and if valid the price is calculated.
•	When the user provides part of the components, all possible configurations with the provided components are listed and price calculated.
•	When the user provides incompatible list of components, they should see an error message indicating what the issue is.

# Reminder
Don't forget to set "pc-store-inventory.json" file to always be copied to the output directory :
Properties -> Copy to Output Directory: Copy always

# Some test input : 
### Test 1:
12500  
12500, 5  
12500, GS8GB  
12500, KS16GB  
12500, KS16GB, 4  
12500, KS16GB, MSIX570  
12500, KS16GB, MSIZ690  
Yes  
### Test 2:
Configurations  
0  
33  
29  
Yes  
