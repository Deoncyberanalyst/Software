
public class CricketPlayersStats {

	// A GTerm class is required to receive input and display data.
	private GTerm gtMain;

	// A GTerm class is required to receive input and display data.
	private GTerm gtSub;

	// This String variable will store the first and last name, which are a sequence
	// of arbitrary characters. An array was chosen to store multiple values in a
	// single variable.
	// The string name "batsmanFullName" was chosen because this variable stores the
	// full name of the batmans. Furthermore, "batsmanFullName"" is easy to refer to
	// and remember.
	/// An alternative variable name such as "fullName" was not chosen because it
	// doesn'tt indicate the category of persons name stored. "driverFullName" is a
	// very specific name.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data can be accessed from the current
	// class.
	private String[] batsmanFullName;

	// This short variable will store a whole number. An integer was not selected
	// because
	// it is too long for this variable. e.g. the maximum number of games
	// played by an international cricket player is 463.
	// Therefore, totalMatches is unlikely to exceed short's maximum.
	// The variable name "totalMatches" was chosen because this variable names
	// stores the total number of matches a batman has played. An array was chosen
	// to store multiple values in a single variable.
	/// An alternative variable name such as "matches" was not used, because
	// "matches" is not specific or descriptive.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.

	private short[] totalMatches;

	// This integer variable will store a whole number - a batman cannot score
	// 6.5 runs. A short data type was not selected because a batsman may
	// accumulates more runs
	// than the maximum value of short(32,767).
	// An array was chosen to store multiple values in a single variable.
	// The variable name "totalRuns" was chosen because this variable names stores
	// the runs a batman has accumulated.
	// An alternative name such as "runsScored" was not chosen because "runs" and
	// "score" have similar definitions. In contrast, "totalRuns" is concise and
	// informative.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.

	private int[] totalRuns;

	// A float is implemented to store the average, as this variable may store
	// decimal
	// values. Utilising a float data type enhances the precision of this value.
	// Short, integer and long data types were not used because they do not support
	// decimals.
	// The variable name "batsmanAverage" was chosen because it stores the batting
	// average,
	// (calculated by diving the totalRuns by totalMatches) .Furthermore, this
	// variable
	// name is easy to remember and reference.
	// An alternative variable name such as "average" was not chosen because
	// the average does not indicate an association with a batman.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.

	private float[] batsmanAverage;

	// A Boolean will be used to store a "true" or "false" data type, to indicate
	// the if the batsman is great.
	// Alternatively, an integer type may be used with values 0 or 1 to indicate
	// true or false.
	// An alternatively variable name such as "isTheBatmanGreat" is too lengthy and
	// prolonged.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.
	private boolean[] greatBatsman;

	// This is a short as it is a whole number. I have not used an integer, long or
	// float because this program is not intended to store large amounts of data.
	// A maximum array length of 32,767 is sufficient for this program.
	// This variable name was chosen because this data point contains the total
	// number of current batsmen stored in the data.
	// This value will determine the current number of batsmen.
	// An alternative variable name such as "currentPositionOfBatman" was not chosen
	// as this name is prolonged, and it does not flow with the of the other
	// variable
	// names.
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.
	private short currentNumBatsman;

	// This is a short as it is a whole number. I have not used an integer, long or
	// float because this program is not intended to store large amounts of data.
	// A maximum array length of 32,767 is sufficient for this program.
	// This value will determine the length of the max number of batsmen.
	// This variable name was chosen because it contains the maximum length of the
	// arrays which contain data.
	// An alternative name such as "MaxBatsman" was not chosen because this variable
	// requires similarity to its parallel (currentNumBatsman).
	// Locality: object level variable because this data is sensitive and needs to
	// be hidden from other classes. This data (variable) can be accessed only with
	// methods in the current class.
	private short maxNumBatsman;

	// Formulation: There isn't a simpler method of initialising variable. The
	// following is the standard, object orientated programming (OOP) method of
	// initialising variables in the constructor.
	// Inclusion: I have initialised object level variables below to adhere to
	// standard OOP practice.
	// Sequence: I have placed the constructor as the first method because it makes
	// the code look clean and makes it easy to debug.
	// Exclusion: I have not included the GUI setup in the constructor, because the
	// GUI method is different to the initialising the object level variables.
	// Furthermore,
	// having a separate method for the GUI allows flexibility to call the GUI
	// method repeatedly in other methods.
	// Below is the constructor. It initialises all object member variables.
	public CricketPlayersStats() {
		this.currentNumBatsman = 0;
		this.maxNumBatsman = 1;
		this.batsmanFullName = new String[this.maxNumBatsman];
		this.totalMatches = new short[this.maxNumBatsman];
		this.totalRuns = new int[this.maxNumBatsman];
		this.batsmanAverage = new float[this.maxNumBatsman];
		this.greatBatsman = new boolean[this.maxNumBatsman];
		this.gtMain = new GTerm(600, 400);
		this.gtSub = new GTerm(550, 600);
		setGUI();
	}

	// This method creates the graphical user interface (GUI) of the program.
	// Formulation: There is not a simpler way to achieve the requirements of this
	// code.
	// Inclusion: This code block creates the GUI for this program.
	// Sequence: This code creates the GUI beginning from the top to bottom, because
	// it minimises the use of .setXY().
	// Exclusion: This code is separate from other variables, because having the GUI
	// creation as a standalone method allows flexibility to call in multiple times.
	public void setGUI() {
		this.gtSub.setFontColor(255, 0, 0);
		this.gtSub.addTable(525, 600, "Player Name\tMatches\tRuns\tAverage\tGreat Batsman");
		this.gtMain.setFontSize(20);
		this.gtMain.setXY(100, 0);
		this.gtMain.setFontColor(255, 0, 0);
		this.gtMain.println("Cricket Player Statistics");
		this.gtMain.setFontSize(14);
		this.gtMain.setFontColor(0, 0, 0);
		this.gtMain.setXY(0, 40);
		this.gtMain.println("Player Name:");
		this.gtMain.setXY(120, 40);
		this.gtMain.addTextField("", 150);
		this.gtMain.setXY(0, 65);
		this.gtMain.println("Total Matches:");
		this.gtMain.setXY(120, 65);
		this.gtMain.addTextField("", 150);
		this.gtMain.setXY(0, 90);
		this.gtMain.println("Total runs");
		this.gtMain.setXY(120, 90);
		this.gtMain.addTextField("", 150);
		this.gtMain.setXY(0, 165);
		this.gtMain.addButton("Add Player", this, "addBatsman");
		this.gtMain.addButton("Remove player", this, "removeBatsman");
		this.gtMain.addButton("Edit", this, "editPlayer");
		this.gtMain.setFontSize(11);
		this.gtMain.setXY(0, 310);
		this.gtMain.println("1.What's a great batmans? \nA batman with an average above 47.5");
		this.gtMain.println("");
		this.gtMain.println("2.How is average calculated?\nDivide the total number of runs by matches (Runs/Matches)");
		this.gtMain.setXY(200, 0);
		// Reference: pngkit. 2020. Welcome To Jai Sri Ram Cricket Club. [online]
		// Available at:
		// <https://www.pngkit.com/bigpic/u2y3w7t4u2t4e6t4/> [Accessed 29 October 2020].
		this.gtMain.addImageIcon("cricketimage.png");
	}

	// This method is invoked when the "Add Player" button is pressed. This method
	// utilises arrays to store user data. The program first checks the length of
	// the current
	// array and determines if there are sufficient space to add another value. If
	// there is not space, a new and larger array is created. Then the program
	// inputs
	// the new value to the end of the larger array. Next the original array is
	// assigned to the larger array.

	// This code below adds data to the table.
	// Formulation: There is not a simpler method of adding a record to an array, as
	// Java arrays cannot resize after it is constructed. Programmers are limited in
	// their ability to manipulate
	// arrays, without implementing work arounds. In this scenario, to add a
	// record we create a new array and refer it back to the original array.
	// Inclusion: As with all methods, they are separated to make them easier to
	// read, debug and look "clean".
	/// Sequence: This code checks if the array length will exceed is maximum
	// capacity. Then only will it create a new array. Otherwise, the code will
	// simply add a new value,
	// to the existing array.
	// Exclusion: this code can only add a batsman. This is to make the code look
	// clean, easy to debug and follow OOP practice.
	public void addBatsman() {

		// This condition checks with if any of the text fields are empty.
		// The program will proceed to add a batsman's record only when all three
		// textfields contain a value.
		// An alterntaive conditions is using the .equalsIgnoreCase("") to check if the
		// text field is empty.
		if ((this.gtMain.getTextFromEntry(0).isEmpty()) || (this.gtMain.getTextFromEntry(1).isEmpty())
				|| (this.gtMain.getTextFromEntry(2).isEmpty())) {

		} else {

			// The following variable names were chosen as, they are like the object
			// member
			// variables (batsmanFullName, totalMatches, totalRuns, average, greatBatsman).
			// The difference being the variables below variables have "new" in the
			// beggining to indicate a brand-new addition to the arrays.
			// The data type justification are the same as the object member variables,
			// except these variables are not arrays.
			// An alternative initial to the variable name such as "latest" was not selected
			// because it does not flow well with the other variable names.
			// Locality: Below are method-level declarations. This is because these
			// variables purpose will not extend past this method. They are only useful in
			// this method.

			String newBatsmanFullName = this.gtMain.getTextFromEntry(0);
			short newTotalMatches = Short.parseShort((this.gtMain.getTextFromEntry(1)));
			int newTotalRuns = Integer.parseInt((this.gtMain.getTextFromEntry(2)));
			float newBatsmanAverage = Float.parseFloat(this.gtMain.getTextFromEntry(2))
					/ Float.parseFloat(this.gtMain.getTextFromEntry(1));
			boolean newGreatBatsman = checkIfGreat(this.gtMain.getTextFromEntry(2), this.gtMain.getTextFromEntry(1));

			// The array expand if the user adds data to an already full array.
			// Another alternative condition includes checking if the maxNumBatsman is
			// higher or equals to currentNumBatsman.
			// However, the current condition is ideal, as it is easy to read and
			// understand.
			if (this.currentNumBatsman >= this.maxNumBatsman) {
				this.maxNumBatsman *= 2;

				// The following variable names were chosen because the variables are similar
				// the object member variables (fullName, matches, runs, average, greatBatsman).
				// The difference is, the variables below have "larger" in the beginning, to
				// indicate the creation of a larger array.
				// The data type justification are the same as the object member variables
				String[] largerBatsmanFullName = new String[this.maxNumBatsman];
				short[] largerTotalMatches = new short[this.maxNumBatsman];
				int[] largerTotalRuns = new int[this.maxNumBatsman];
				float[] largerBatsmanAverage = new float[this.maxNumBatsman];
				boolean[] largerGreatBatsman = new boolean[this.maxNumBatsman];
				short currentLocation = 0;

				// This while loop is used to create a larger array and then refer the original
				// array to the expanded array.
				// Alternative condition is until currentLocation is equal to the
				// currentNumBatsman
				// (while(currentLocation==this.currentNumBatsman)).
				// Loop condition: this loop will repeat until the counter variable
				// (currentLocation) is more than or equals to the current number of batsman.
				while (currentLocation < this.currentNumBatsman) {

					largerBatsmanFullName[currentLocation] = this.batsmanFullName[currentLocation];
					largerTotalMatches[currentLocation] = this.totalMatches[currentLocation];
					largerTotalRuns[currentLocation] = this.totalRuns[currentLocation];
					largerBatsmanAverage[currentLocation] = this.batsmanAverage[currentLocation];
					largerGreatBatsman[currentLocation] = this.greatBatsman[currentLocation];
					currentLocation += 1;

				}
				this.batsmanFullName = largerBatsmanFullName;
				this.totalMatches = largerTotalMatches;
				this.totalRuns = largerTotalRuns;
				this.batsmanAverage = largerBatsmanAverage;
				this.greatBatsman = largerGreatBatsman;
			}

			// This method of adding values to a table is simple and effective.
			// The main block is towards the end of my code, as it will be important to
			// first
			// setup the variables before executing a process.
			// Furthermore, if the array size is appropriate, it is unnecessary to extend
			// the length of the array.
			this.batsmanFullName[this.currentNumBatsman] = newBatsmanFullName;
			this.totalMatches[this.currentNumBatsman] = newTotalMatches;
			this.totalRuns[this.currentNumBatsman] = newTotalRuns;
			this.batsmanAverage[this.currentNumBatsman] = newBatsmanAverage;
			this.greatBatsman[this.currentNumBatsman] = newGreatBatsman;
			this.currentNumBatsman += 1;

			this.gtMain.setTextInEntry(0, "");
			this.gtMain.setTextInEntry(1, "");
			this.gtMain.setTextInEntry(2, "");
			refreshTable();
		}
	}

	// This method was introduced to minimise code duplication.
	// This method receives two parameters (runs & matches). Then runs/matches are
	// divided, and the outcome is compared to find if it exceeds 47.5.
	// Then the method returns a true or false Boolean.

	// Formulation: This method could be achieved in several ways, such as receiving
	// int parameters, or by returning a Boolean in the middle of method. However,
	// the current process
	// adheres to clean coding (it is not "spaghetti code").
	// Sequence: The current process creates the required variables and then enters
	// these variables into a condition. This sequence is required for the code to
	// function.
	// Inclusion & exclusion: This is to make the code look clean, easy to debug and
	// follow OOP practice.
	public boolean checkIfGreat(String runs, String matches) {

		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// Strings runs, and matches are parameter level declarations. This is because
		// runs and matches are provided by another method for calculation and them
		// correspond to the variables that are passed.

		// I have chosen a float variable to store the average because this variable
		// may store a value with decimal points.
		// Using a variable type which does not decimals is inappropriate
		// (such as an integer).
		// I have chosen "battingAverage" as the variable name as this variable will
		// store the batting average of the batman (runs/matches).
		// An alternative variable name such as "average" cannot be used as this name is
		// too vague.
		float battingAverage = Float.parseFloat(runs) / Float.parseFloat(matches);

		// I have chosen a Boolean because this variable needs to contain a true or
		// false statement.
		// I can also select a string for this purpose, however a Boolean more suitable.
		// I have chosen the following name for the variable because it indicates the
		// status of the batmans. e.g. "Is batmans 1 a great batman:" result:
		// true(boolean).
		// An alternative variable name such as "greatBatsman" cannot be used as this
		// name is too similar to the
		// object member variable.
		boolean greatBatsmanStatus = false;

		// Alternative condition can be if(run==0 || matches==0 || battingAverage <
		// 47.5) then greatBatmanStatus = false. Otherwise return true.
		// This means the first condition would check if the runs equals to zero, if the
		// matches equals
		// to zero or if the battingAverage is less than 47.5 and if any of these
		// conditions are met,
		// return false. If none of these conditions are met, then return true.
		if (Float.parseFloat(runs) == 0 || Float.parseFloat(matches) == 0) {
			greatBatsmanStatus = false;

		} else if (battingAverage < 47.5) {
			greatBatsmanStatus = false;

		} else if (battingAverage >= 47.5) {
			greatBatsmanStatus = true;
		}

		return greatBatsmanStatus;
	}

	// This method allows the GTerm table to display the values inside the object
	// member arrays. This process is done by first clearing the GTerm table. Then,
	// all object member arrays are converted to a string and added into the table
	// individually. A while loop is implemented for this purpose.

	// This block of code adds data to the tables.
	// This method clears all current information on the table and then prints the
	// new data to it.
	// Formulation: There are several methods of achieving the same requirement,
	// such as returning individual values or returning a string containing all the
	// data.
	// Inclusion & exclusion: This method can clear the GTerm table and then in adds
	// information to the table. Any other function is not included in this method
	// to simply coding.
	public void refreshTable() {

		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		this.gtSub.clearRowsOfTable(0);

		// I have chosen a short as it is a whole number. This variable used to identify
		// the
		// current location of the array.
		// I have not selected an integer because the value of current batsman is
		// limited is the same as short data type.
		// I have chosen this variable name because it provides the programmer an
		// indication to what the data this variable will contain. e.g. this variable
		// shows the current index location of the array.
		short currentLocation = 0;

		// The code below prints the name, licence number, test score, condition and
		// pass.
		// Alternative while(currentLocation<this.currentNumDrivers)
		// A possible alternative for this condition could be counting
		// Loop condition: This loop will repeat until the currentLocation is less
		// than or equals to the current number of batsman.
		while (currentLocation < this.currentNumBatsman) {
			this.gtSub.addRowToTable(0,
					this.batsmanFullName[currentLocation] + "\t" + this.totalMatches[currentLocation] + "\t"
							+ this.totalRuns[currentLocation] + "\t" + this.batsmanAverage[currentLocation] + "\t"
							+ this.greatBatsman[currentLocation]);
			currentLocation += 1;
		}
	}

	// This method allows to remove the selected index value to be removed. The
	// process for is add all values of the array except for the selected index row.

	// Formulation: With our limited knowledge of Java, I think the process of
	// creating a new array and then excluding the removed array is the best way of
	// removing a record
	// Inclusion: This method has one function: to remove a selected record from the
	// array.
	// Exclusion: this method can only remove a record, this is intention, because
	// it makes the program easier to understand for reader and makes it easier to
	// debug.
	public void removeBatsman() {
		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// I have chosen a short as it is a whole number, used to identify the current
		// location of the array. I have not chosen an integer because the value of
		// currentLocation
		// is unlikely to exceed maximum value of short.
		// I have chosen this variable name because it provides the programmer an
		// indication to what the data this variable will contain. e.g. this variable
		// shows the current index location of the array.
		// An alternative variable name such as "counter" was not chosen because it is
		// not specific enough.
		short currentLocation = 0;

		// This loop will repeat until the counter variable (currentLocation) is more
		// than, or equals to the current batsman and if the current batsman
		// is not the index of the selected batsman.
		while (currentLocation < this.currentNumBatsman
				&& !(currentLocation == this.gtSub.getIndexOfSelectedRowFromTable(0)))
			currentLocation += 1;

		// Condition: while the new index position is less than the total number of
		// records in the array is list, repeat the code below.
		// Alternatives conditions: this.newArraySize != this.numRecords
		if (currentLocation < this.currentNumBatsman) {

			// This loop will repeat until the currentLocation(counter) is more than, or
			// equals to the current number of batsman minus one.
			while (currentLocation < this.currentNumBatsman - 1) {
				this.batsmanFullName[currentLocation] = this.batsmanFullName[currentLocation + 1];
				this.totalMatches[currentLocation] = this.totalMatches[currentLocation + 1];
				this.totalRuns[currentLocation] = this.totalRuns[currentLocation + 1];
				this.batsmanAverage[currentLocation] = this.batsmanAverage[currentLocation + 1];
				this.greatBatsman[currentLocation] = this.greatBatsman[currentLocation + 1];
				currentLocation += 1;
			}
			this.currentNumBatsman -= 1;
			refreshTable();
		}
	}

	// This method allows the user to edit the selected index. The user must first
	// press the "Edit" button and then type in what they wish to edit (name,
	// matches or
	// runs). Then the GUI will clear and ask the user to type a new value. When the
	// "Complete Edit" button is pressed, a new method is invoked.

	// Formulation: This method allows us to edit a specific array, by selecting its
	// index location and then replacing it with a new variable. I believe this is
	// an effective way of editing data. I believe the demonstrated method below is
	// ideal, as it is memory efficient and easy to follow.
	// Inclusion: As with all other methods, they are separated to make them easier
	// to read, debug and look "clean".
	public void editPlayer() {
		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// I have chosen a string because it will store an arbitrary list of
		// characters. Alternatively, I could use an integer and match the integer to
		// a method.
		// This variable name was chosen as it clearly describes the content it stores,
		// which is the user inserts to the GTerm method .getInputString()
		// An alternative variable name such as "input" was not selected because I felt
		// that it was not very specific and there are multiple inputs occurring in this
		// program
		// (e.g. input data into a table and input edited fields to the table).
		String userInput = this.gtSub.getInputString("Do you want to edit Name, Matches or Runs?");

		// This condition checks string input by the user.
		// Alternative conditions: I can request the user to input a numerical digit as
		// input
		// and then create if statements according to this. For example: User inputs 1,
		// to edit
		// the name. The condition for this would be, if the user input equals to zero,
		// then
		// execute the code for editing name.
		if (userInput.equalsIgnoreCase("Full Name") || userInput.equalsIgnoreCase("Name")) {
			this.gtMain.clear();
			this.gtMain.setXY(50, 50);
			this.gtMain.setFontSize(16);
			this.gtMain.println("Enter new name for selected record");
			this.gtMain.addTextField("", 150);
			this.gtMain.setFontSize(11);
			this.gtMain.setXY(50, 90);
			this.gtMain.addButton("Complete Edit", this, "editName");

		} else if (userInput.equalsIgnoreCase("Total Matches") || userInput.equalsIgnoreCase("Matches")) {
			this.gtMain.clear();
			this.gtMain.setXY(50, 50);
			this.gtMain.setFontSize(16);
			this.gtMain.println("Enter total matches for selected record");
			this.gtMain.addTextField("", 150);
			this.gtMain.setFontSize(11);
			this.gtMain.setXY(50, 90);
			this.gtMain.addButton("Complete Edit", this, "editMatches");

		} else if (userInput.equalsIgnoreCase("Total runs") || userInput.equalsIgnoreCase("Runs")) {
			this.gtMain.clear();
			this.gtMain.setXY(50, 50);
			this.gtMain.setFontSize(16);
			this.gtMain.println("Enter total runs for selected record");
			this.gtMain.addTextField("", 150);
			this.gtMain.setFontSize(11);
			this.gtMain.setXY(50, 90);
			this.gtMain.addButton("Complete Edit", this, "editRuns");
		}
	}

	// This method replaces the selected index location of the totalMatches array
	// with a new value.

	// Formulation: This method allows us to edit a specific array (totalMatches
	// array), by selecting its index location and then replacing it with a new
	// value. I believe this is
	// an effective way of editing data.
	// Inclusion: This method can only replace the batsmanFullName array with a new
	// value. This simplifies coding and debugging.
	// Sequence: The coding is in this order for it to function properly.
	// Exclusion: This method is separate from editing runs and matches because
	// having a different methods allows the programmer more flexibility to be more
	// innovative.
	public void editMatches() {
		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// I have chosen an integer because it will store a whole number and because
		// the GTerm method .getIndexOfSelectedRowFromTable() does not yield a decimal.
		// Therefore, this is the best and most appropriate variable type for this
		// method.
		// This variable name was chosen because it is clear and concise name.
		// Furthermore, it indicates the data it stores, which is the index of the
		// selected row from GTerm table.
		int indexSelected = this.gtSub.getIndexOfSelectedRowFromTable(0);
		this.totalMatches[indexSelected] = Short.parseShort(this.gtMain.getTextFromEntry(0));
		float editMatches = this.totalMatches[indexSelected];
		float editRuns = this.totalRuns[indexSelected];
		this.batsmanAverage[indexSelected] = (editRuns / editMatches);
		this.greatBatsman[indexSelected] = checkIfGreat(Integer.toString(this.totalMatches[indexSelected]),
				Integer.toString(this.totalRuns[indexSelected]));
		this.gtMain.clear();
		this.gtSub.clear();
		setGUI();
		refreshTable();
	}

	// This method replaces the selected index location of the batsmanFullName array
	// with a new value.
	// Formulation: This method allows us to edit a specific array (batsmanFullName
	// array), by selecting its index location and then replacing it with a new
	// value. I believe this is
	// an effective way of editing data.
	// Inclusion: This method can only replace the batsmanFullName array with a new
	// value. This simplifies coding and debugging.
	// Sequence: The coding is in this order for it to function properly.
	// Exclusion: This method is separate from editing runs and matches because
	// having a different methods allows the programmer more flexibility to be more
	// innovative.
	public void editName() {
		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// I have chosen an integer because it will store a whole number. Furthermore,
		// the GTerm method .getIndexOfSelectedRowFromTable() does to yield a decimal
		// number.
		// Therefore this is the best and most appropriate variable type for this
		// method.
		// This variable name was chosen because it is clear and concise name.
		// Furthermore, it indicates the data it stores, which is the index of the
		// selected row from GTerm table.

		int indexSelected = this.gtSub.getIndexOfSelectedRowFromTable(0);
		this.batsmanFullName[indexSelected] = this.gtMain.getTextFromEntry(0);
		this.gtMain.clear();
		this.gtSub.clear();
		setGUI();
		refreshTable();
	}

	// This method replaces the selected index location of the editRuns array with a
	// new value.

	// Formulation: This method allows us to edit a specific array (totalRuns
	// array), by selecting its index location and then replacing it with a new
	// value. I believe this is
	// an effective way of editing data.
	// Inclusion: This method can only replace the batsmanFullName array with a new
	// value. This simplifies coding and debugging.
	// Sequence: The coding is in this order for it to function properly.
	// Exclusion: This method is separate from editing name and matches because
	// having a different methods allows the programmer more flexibility to be more
	// innovative.
	public void editRuns() {
		// Locality: Below are method-level declarations. This is because these
		// variables purpose will not extend past this method. They are only useful in
		// this method.

		// I have chosen an integer because it will store a whole number. Furthermore,
		// the GTerm method .getIndexOfSelectedRowFromTable() does to yield a decimal
		// number.
		// Therefore, this is the best and most appropriate variable type for this
		// method. Alternatively, I could use a string for this purpose, however a
		// This variable name was chosen because it is clear and concise name.
		// Furthermore, it indicates the data it stores, which is the index of the
		// selected row from GTerm table.
		int indexSelected = this.gtSub.getIndexOfSelectedRowFromTable(0);
		this.totalRuns[indexSelected] = Integer.parseInt(this.gtMain.getTextFromEntry(0));
		float editMatches = this.totalMatches[indexSelected];
		float editRuns = this.totalRuns[indexSelected];
		this.batsmanAverage[indexSelected] = (editRuns / editMatches);
		this.greatBatsman[indexSelected] = checkIfGreat(Integer.toString(this.totalRuns[indexSelected]),
				Integer.toString(this.totalMatches[indexSelected]));
		this.gtMain.clear();
		this.gtSub.clear();
		setGUI();
		refreshTable();
	}

	public static void main(String[] args) {
		CricketPlayersStats a2obj = new CricketPlayersStats();
	}
}
