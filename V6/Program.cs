using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace V6
{
    class Program
    {

		public static string filepath;

        static void Main(string[] args)
        {

			filepath = args[0];

			if (!File.Exists(filepath))
			{

				Console.Clear();
                File.WriteAllText(filepath, "ID,FirstName,LastName,Savings,Password,Data");
				Console.WriteLine("\nThe file of records has been created!\n");
			
			}

            else
            {

                string[] records = File.ReadAllLines(filepath);

                List<string> list = new List<string>();

                for (int line = 0; line < records.Length; line++) list.Add(records[line]);

                foreach (var item in records)
                {

                    if (records.ToList().IndexOf(item) > 0) Person.People.Add(Person.FromCsvFile(item));
                    
                }

            }

			while (true)
            {

                Menu();

            }
		}

        public static void Menu()
        {

            Console.WriteLine("\n 	DataRegister App! v6.0.0\n========================================");
            Console.WriteLine("\n1. Registry a record");
            Console.WriteLine("2. View the file of records");
            Console.WriteLine("3. Record Finder");
            Console.WriteLine("4. Record Remover");
            Console.WriteLine("5. Record Editer");
            Console.WriteLine("6. Save to Record's File");
            Console.WriteLine("7. Exit\n");

            string rightselec = Console.ReadLine();

            switch (rightselec)
            {

                case "1":

                    Console.Clear();
                    Procedure(Person.People, false);

                    break;

                case "2":

                    Console.Clear();
                    ToList(Person.People);

                    break;

                case "3":

                    Console.Clear();
                    Finder(Person.People);

                    break;

                case "4":

                    Console.Clear();
                    Delete();

                    break;

                case "5":

                    Console.Clear();
                    Procedure(Person.People, true);

                    break;

                case "6":

                    Console.Clear();
                    Person.SaveToCsv();

                    break;

                case "7":

                    Console.Clear();
                    Environment.Exit(0);

                    break;

                default:

                    Console.Clear();
					Console.WriteLine("\nSomething went wrong, try again.\n");
                    Menu();

                    break;

            }

        }

		public static void Procedure(List<Person> people, bool editing)
		{

            while (true)
            {

                char sex, maritalsts, grade;
                int csex, cmaritalsts, cgrade = 0;
                string fname, lname, saving, fpw, spw, id = "";
                Person person = new Person("", "", "", 0, "", 0);

                if (!editing) id = numInput("\nEnter the ID: ");
                else
                {

                    id = numInput("\nEnter the ID of the record you want to edit: ");
                    Console.WriteLine();
                    person = Finder(people, id);
                    if (person.Id == "") return;
                    else id = person.Id;
                    Console.WriteLine("\nProceed to make the changes!");

                }

                if (UniqueID(id) && !editing)
                {

                    Console.WriteLine("\nThis ID has already been recorded.");
                    break;

                }

                fname = stdInput("\nEnter the First Name: ");
                lname = stdInput("\nEnter the Last Name: ");
                int age = Convert.ToInt32(numInput("\nEnter the Age: "));
                while (age < 7 || age > 120) age = Convert.ToInt32(numInput("\nEnter the Age: "));

                do
                {

                    Console.WriteLine("\nEnter the Sex: [M|F]");
                    sex = Convert.ToChar(Console.ReadLine().ToUpper());
                    
                } while (sex != 'M' && sex != 'F');

                if (sex == 'M') csex = 1;
                else csex = 0;

                do
                {

                    Console.WriteLine("\nEnter the Marital Status: [S|M]");
                    maritalsts = Convert.ToChar(Console.ReadLine().ToUpper());
                    
                } while (maritalsts != 'S' && maritalsts != 'M');

                if (maritalsts == 'M') cmaritalsts = 1;
                else cmaritalsts = 0;

                do
                {

                    Console.WriteLine("\nEnter the Education Level: [I|M|G|P]");
                    grade = Convert.ToChar(Console.ReadLine().ToUpper());
                    
                } while (grade != 'I' && grade != 'M' && grade != 'G' && grade != 'P');

                switch (grade)
                {

                    case 'M':
                        cgrade = 1;
                        break;

                    case 'G':
                        cgrade = 2;
                        break;

                    case 'P':
                        cgrade = 3;
                        break;

                }

                saving = decInput("\nEnter the Savings: ");
                do
                {
                    
                    fpw = pwInput("\nEnter the Password: ");
                    spw = pwInput("\nConfirm the Password: ");
                    if (fpw != spw) Console.WriteLine("\nPasswords do not match!");

                } while (fpw != spw);

                int data = Encode(age, sex, maritalsts, grade);
                
                Console.WriteLine("\nSave [S]; Discard[D]; Exit[E]");
                string Selection = Console.ReadLine();

                switch (Selection.ToLower())
                {

                    case "s":

                        Console.Clear();
                        if (!editing)
                        {

                            var nperson = Person.FromCsvFile($"{id},{fname},{lname},{saving},{fpw},{data}");
                            Person.People.Add(nperson);
                            Console.WriteLine("\nRecord registered correctly!\n");

                        }

                        else
                        {

                            if (person.FullName != fname + " " + lname) Console.WriteLine("\nChanges in the Full Name made successfully!");
                            
                            if (person.Age != age) Console.WriteLine("\nChanges in the Age made successfully!");

                            if ((int)person.Sex != csex) Console.WriteLine("\nChanges in the Sex made successfully!");

                            if ((int)person.MaritalStatus != cmaritalsts) Console.WriteLine("\nChanges in the Marital Status made successfully!");

                            if ((int)person.Grade != cgrade) Console.WriteLine("\nChanges in the Education Level made successfully!");

                            if (person.Savings != Convert.ToDouble(saving)) Console.WriteLine("\nChanges in the Savings made successfully!");

                            if (person.Password != fpw) Console.WriteLine("\nChanges in the Password made successfully!");

                            if ((person.FullName == fname + " " + lname) && (person.Age == age) && ((int)person.Sex == csex) && ((int)person.MaritalStatus == cmaritalsts) && ((int)person.Grade == cgrade) && (person.Savings == Convert.ToDouble(saving)) && (person.Password == fpw))
                            {

                                Console.WriteLine("\nIt appears no changes has been made.");

                            }

                            var nperson = Person.FromCsvFile($"{id},{fname},{lname},{saving},{fpw},{data}");
                            var i = people.FindIndex(a => a.Id == nperson.Id);
                            people[i] = nperson;
                            
                        }

                        break;

                    case "d":

                        Console.Clear();
                        if (!editing) Procedure(Person.People, false);
                        else Procedure(Person.People, true);

                        break;

                    case "e":

                        Console.Clear();
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine("\nSomething went wrong, try again.");

                        break;

                }

                break;

            }


		}

        public static void ToList(List<Person> People)
        {

            Console.Clear();

            foreach (var item in People)
            {

                Console.WriteLine(item);
                
            }

        }

        static Person Finder(List<Person> Person, string id = null)
        {
            
            if (id == null) id = numInput("\nEnter the ID of the record you want:");

            Person person = new Person("", "", "", 0, "", 0);

            foreach (var item in Person)
            {

                if (item.Id == id)
                {

                    person = item;
                    Console.WriteLine(person);

                }
                
            }

            if (person.Id == "") Console.WriteLine("That record doesn't exist.");

            return person;

        }

        public static bool UniqueID(string id)
        {

            bool verify = false;
            var content = File.ReadAllLines(filepath);

            foreach (var item in content)
            {

                var exists = item.Split(",");

                if (exists[0] == id)
                {

                    return !verify;

                }
                
            }

            return verify;

        }

        public static void Delete()
        {

            Person person = Finder(Person.People);

            if (person.Id == "") return;

            Console.WriteLine("\nAre you positive you want to delete this record? [Y|N]");

            char path = Convert.ToChar(Console.ReadLine().ToLower());

            switch (path)
            {

                case 'y':

                    try
                    {

                        Person.People.RemoveAll(x => x.Id.Trim() == person.Id);

                    }

                    catch (Exception exc)
                    {

                        throw new ApplicationException("\nAn error has ocurred: ", exc);

                    }

                    break;

                case 'n':
                    break;

                default:

                    Console.WriteLine("\nSomething went wrong!");

                    break;

            }

        }

        public static string pwInput(string writeline)
        {

            Console.WriteLine(writeline);

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else
                {

                    if (key.KeyChar != 44)
                    {

                        input += key.KeyChar.ToString();
                        Console.Write("*");

                    }

                }

            }

            return input;

        }

        public static string numInput(string writeline)
        {

            Console.WriteLine(writeline);

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar))
                {

                    if (input.Length <= 11)
                    {

                        input += key.KeyChar.ToString();
                        Console.Write(key.KeyChar);

                    }

                }

            }

            return input;

        }

        public static string decInput(string writeline)
        {

            Console.WriteLine(writeline);

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

                else if (key.KeyChar == 46)
                {

                    if (!input.Contains("."))
                    {

                        input += key.KeyChar.ToString();
                        Console.Write(key.KeyChar);

                    }

                }

            }

            return input;

        }

        public static string stdInput(string writeline)
        {

            Console.WriteLine(writeline);

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (!char.IsDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

            }

            return input;

        }

        public static int Encode(int age, char sex, char maritalsts, char grade)
        {

            int data = age << 4;

            if (sex == 'M')
            {

                data = data | 8;

            }

            if (maritalsts == 'M')
            {

                data = data | 4;

            }

            if (grade == 'M')
            {

                data = data | 1;

            }

            else if (grade == 'G')
            {

                data = data | 2;

            }

            else if (grade == 'P')
            {

                data = data | 3;

            }

            return data;

        }

        /* public static string Decode(int data)
        {

            int age = data >> 4;
            
            int opS = data - (age << 4);

            int sex = opS >> 3;
            var sexP = "";

            if (sex == 1)
            {

                sexP = "Male";

            }

            else
            {

                sexP = "Female";

            }

            int opM = opS - (sex << 3);

            int maritalsts = opM >> 2;
            var maritalstsP = "";

            if (maritalsts == 1)
            {

                maritalstsP = "Married";

            }

            else
            {

                maritalstsP = "Single";

            }

            int grade = opM - (maritalsts << 2);
            var gradeP = "";

            if (grade == 1)
            {

                gradeP = "Medium";

            }

            else if (grade == 2)
            {

                gradeP = "Grade";

            }

            else if (grade == 3)
            {

                gradeP = "Post-Grade";

            }

            else
            {

                gradeP = "Initial";

            }

            return $",{age},{sexP},{maritalstsP},{gradeP}";

        } */

    }
}
