--11
CREATE FUNCTION udf_RoomsWithTourists(@name VARCHAR(40))
RETURNS INT 
AS
BEGIN
	DECLARE @result INT
	SET @result = 
	(
		SELECT SUM(b.AdultsCount + b.ChildrenCount) FROM Rooms AS r
		JOIN Bookings AS b ON b.RoomId = r.Id
		JOIN Tourists AS t ON t.Id = b.TouristId
		WHERE r.[Type] = @name
	)
	IF @result IS NULL
	SET @result = 0
	RETURN @result
END;
--12
CREATE PROCEDURE usp_SearchByCountry(@country NVARCHAR(50))
AS
BEGIN
	SELECT t.[Name], t.[PhoneNumber], t.Email, COUNT(b.Id) AS CountOfBookings
	FROM Countries AS c
	JOIN Tourists AS t ON t.CountryId = c.Id
	JOIN Bookings AS b ON b.TouristId = t.Id
	WHERE c.[Name] = @country
	GROUP BY t.[Name], t.[PhoneNumber], t.Email
	ORDER BY t.[Name] ASC, COUNT(b.Id) DESC 
END;