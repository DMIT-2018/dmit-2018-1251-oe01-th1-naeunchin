<Query Kind="Statements">
  <Connection>
    <ID>af48b9c4-66cc-4485-b095-35c21e032a6b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>NAEUN-XPS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//=====Exercise: Simple LINQ======
//Student Name: Na Eun Chin

//Question 1:
//Every upcoming club activity (January 1, 2025 and onward), no campus venue == "Scheduled room", no name == "BTech Club" 
//Display start date, venue name, hosting club name, activity title, order by ascending start date 

ClubActivities
	.Where(x => x.StartDate >= new DateTime(2025, 01, 01)
				&& x.CampusVenue.Location != "Scheduled Room"
				&& x.Name != "BTech Club Meeting")
	.Select(x => new 
	{
		StartDate = x.StartDate,
		Location = x.CampusVenue.Location,
		Club = x.Name,
		Activity = x.Name
	})
	.OrderBy(x => x.StartDate)
	.Dump();

//Question 2:
//Translate school code into user-friendly name, count how many courses are mandatory / optional, show only programs with at least (>=) 22 required courses
//Display full school name, program name, required courses count(), optional courses count(), order by ascending program name

Programs
	.Select(x => new 
	{
		School = x.SchoolCode == "SAMIT" ? "School of Advance Media and IT" :
				x.SchoolCode == "SEET" ? "School of Electrical Engineering Technology" : "Unknown",
		Program = x.ProgramName,
		RequiredCourseCount = x.ProgramCourses.Count(x => x.Required),
		OptionalCourseCount = x.ProgramCourses.Count(x => !x.Required)
		
	})
	.Where(x => x.RequiredCourseCount >= 22)
	.OrderBy(x => x.Program)
	.Dump();


//Question 3:
//Filter students with 0 entries in StudentPayments, country != "Canada", 
//Display student number, country name in uppercase, full name, clubmembership count (none if 0 clubs), Order by last name ascending

Students
	.Where(x => x.Countries.CountryName != "CANADA" && x.StudentPayments.Count() == 0)
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		StudentNumber = x.StudentNumber,
		CountryName = x.Countries.CountryName,
		FullName = $"{x.FirstName} {x.LastName}",
		ClubMembershipCount = x.ClubMembers.Count() == 0 ? "None" : x.ClubMembers.Count().ToString()
	})
	.Dump();
	

//Question 4:


//Question 5: