using System;

// How to implement: https://msdn.microsoft.com/en-us/library/87d83y5b.aspx

/// <summary>
/// Developed by: Peao (rngs);
/// Gerenic definiton of a PreCondition;
/// </summary>
public interface IPreCondition
{
	int identifier { get; }
	string name { get; set; }
	bool checkIfMatches(User userProfile);
}