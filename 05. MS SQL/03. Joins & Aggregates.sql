-- 05
SELECT FORMAT(ArrivalDate, 'yyyy-MM-dd') AS ArrivalDate,
	AdultsCount,
	ChildrenCount
FROM Bookings
JOIN Rooms AS r ON r.Id = Bookings.RoomId
ORDER BY r.Price DESC, ArrivalDate ASC
--06
SELECT Hotels.Id, [Name] FROM Hotels
JOIN HotelsRooms AS hr ON hr.HotelId = Hotels.Id
JOIN Rooms AS r ON r.Id = hr.RoomId
JOIN Bookings AS b ON b.HotelId = Hotels.Id
WHERE r.[Type] = 'VIP Apartment'
GROUP BY Hotels.Id, [Name]
ORDER BY COUNT(b.Id) DESC
--07
SELECT t.Id, t.[Name], t.PhoneNumber FROM Tourists AS t
LEFT JOIN Bookings AS b ON b.TouristId = t.Id
WHERE b.ArrivalDate IS NULL AND b.DepartureDate IS NULL
ORDER BY t.[Name]
--08
SELECT TOP(10) h.[Name] AS HotelName,
	d.[Name] AS DestinationName,
	c.[Name] AS CountryName
FROM Bookings AS b
JOIN Hotels AS h ON h.Id = b.HotelId
JOIN Destinations AS d ON d.Id = h.DestinationId
JOIN Countries AS c ON c.Id = d.CountryId
WHERE b.ArrivalDate < '2023-12-31' AND b.HotelId % 2 <> 0
ORDER BY c.[Name], b.ArrivalDate
--09
SELECT h.[Name] AS HotelName,
	r.Price AS RoomPrice
FROM Tourists AS t
JOIN Bookings AS b ON b.TouristId = t.Id
JOIN Hotels AS h ON h.Id = b.HotelId
JOIN Rooms AS r ON r.Id = b.RoomId
WHERE RIGHT(t.[Name], 2) <> 'EZ'
ORDER BY RoomPrice DESC
--10
SELECT h.[Name] AS HotelName,
	SUM(DATEDIFF(DAY, b.ArrivalDate, b.DepartureDate) * r.Price) AS HotelRevenue
FROM Bookings AS b
JOIN Hotels AS h ON h.Id = b.HotelId
JOIN Rooms AS r ON r.Id = b.RoomId
GROUP BY h.[Name]
ORDER BY HotelRevenue DESC