declare @ds nvarchar(max) = 'AppointmentStatus';
insert into DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('Pending',1,'Pending',@ds,'Pending', newid(), CURRENT_TIMESTAMP),
('Accepted',2,'Accepted',@ds,'Accepted', newid(), CURRENT_TIMESTAMP),
('Rejected',3,'Rejected',@ds,'Rejected', newId(),CURRENT_TIMESTAMP),
('Cancelled',4,'Cancelled',@ds,'Cancelled', newId(), CURRENT_TIMESTAMP),
('Sent',5,'Pending',@ds,'Pending', newId(), CURRENT_TIMESTAMP)

