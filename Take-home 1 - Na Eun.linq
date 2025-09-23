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
//Every upcoming club activity (January 1, 2025 and onward), campus venue != "Scheduled room", name != "BTech Club" 
//Display start date, venue name, hosting club name, activity title
//Order by ascending start date 

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
//Display full school name, program name, required courses count(), optional courses count()
//Order by ascending program name

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
//Display student number, country name in uppercase, full name, clubmembership count (none if 0 clubs)
//Order by last name ascending

Students
	.Where(x => x.Countries.CountryName != "CANADA" && !x.StudentPayments.Any())
	//I'm using OrderBy before Select because LastName is not included in the anonymous dataset 
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
//Filter employees by position == "Instructor", releasedate == null, ClassOffering >= 1
//Order by descending ClassOfferings, Thenby LastName
//Display ProgramName, FullName, WorkLoad (>24 = "High", >8 = "Med", otherwise "Low")

Employees
	.Where(x => x.Position.Description == "Instructor" && x.ReleaseDate == null && x.ClassOfferings.Count() >= 1)
	//I'm using OrderBy before Select because ClassOfferings.Count() is not included in the anonymous dataset 
	.OrderByDescending(x => x.ClassOfferings.Count())
	.ThenBy(x => x.LastName)
	.Select(x => new 
	{
		ProgramName = x.Program.ProgramName,
		FullName = $"{x.FirstName} {x.LastName}",
		WorkLoad = x.ClassOfferings.Count() > 24 ? "High" :
					x.ClassOfferings.Count() > 8 ? "Med" : "Low"	
	})
	.Dump();

//Question 5:
//List club faculty's supervisor, membership size, upcoming activity count
//Display Supervisor (name or "Unknown" for null values), Club (name), MemberCount (# of entries in ClubMembers), Acivities (count or "None Schedule" if ClubActivities.Count() == 0)
//Order by descending member count

Clubs
	.Select(x => new
	{
		Supervisor = x.Employee == null ? "Unknown" : $"{x.Employee.FirstName} {x.Employee.LastName}",
		Club = x.ClubName,
		MemberCount = x.ClubMembers.Count(),
		Activities = x.ClubActivities.Count() == 0 ? "None Schedule" : x.ClubActivities.Count().ToString()
	})
	.OrderByDescending(x => x.MemberCount)
	.Dump();
