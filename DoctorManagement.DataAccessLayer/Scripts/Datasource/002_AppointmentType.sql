declare @ds nvarchar(max) = 'AppointmentType';
insert into DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('DoctorInvitation',1,'Home visit',@ds,'Invitation', newid(), CURRENT_TIMESTAMP),
('DoctorVisit',2,'Site visit',@ds,'Visit', newid(), CURRENT_TIMESTAMP)
