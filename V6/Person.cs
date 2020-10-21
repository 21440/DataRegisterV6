using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Person
{

    public static List<Person> People = new List<Person>();

    public string Id { get; }

    public string Name { get; }

    public string LastName { get; }

    public string FullName => $"{Name} {LastName}";

    public double Savings;
    
    public string Password { get; }

    private int Data = 0;

    public int Age => Data >> 4;
    
    public Sex Sex => (Sex) ((Data & 15) >> 3);

    public MaritalStatus MaritalStatus => (MaritalStatus) ((Data & 7) >> 2);

    public Grade Grade => (Grade) (Data & 3);


    public Person(in string id, in string name, in string lastname, in double savings, in string pw, in int data)
    {

        Id = id;

        Name = name;

        LastName = lastname;

        Savings = savings;

        Password = pw;

        Data = data;

    }

    internal static Person FromCsvFile(string line)
    {
        
        string[] token = line.Split(',');

        return new Person(token[0], token[1], token[2], double.Parse(token[3]), token[4], int.Parse(token[5]));

    }

    internal static void SaveToCsv()
    {
        if(People.Count() > 0)
        {
            File.WriteAllText(V6.Program.filepath, "ID,Name,LastName,Savings,Password,Data");
            foreach (var being in Person.People)
            {
                File.AppendAllText(V6.Program.filepath, $"{Environment.NewLine}{being.Id},{being.Name},{being.LastName},{being.Savings},{being.Password},{being.Data}");
            }
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            return Id.Equals(other.Id);
        }
        return false;
    }

    public override string ToString()
    {
        return $"ID: {Id}; FullName: {FullName}; Age: {Age}; Sex: {Sex}; MaritalStatus: {MaritalStatus}; Grade: {Grade}; Savings: {Savings}; Password: {Password}";
    }

}

public enum Sex
{

    Female = 0,

    Male = 1

}

public enum MaritalStatus
{

    Single = 0,

    Married = 1

}

public enum Grade
{

    Initial = 0,

    Medium = 1,

    Grade = 2,

    Postgrade = 3

}