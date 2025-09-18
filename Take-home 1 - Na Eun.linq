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
//Columns: start date, venue name, hosting club name, activity title, order by ascending start date 

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


//Question 3:


//Question 4:


//Question 5: