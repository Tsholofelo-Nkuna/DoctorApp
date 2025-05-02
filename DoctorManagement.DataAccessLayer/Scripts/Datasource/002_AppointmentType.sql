declare @ds nvarchar(max) = 'AppointmentType';
insert into DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('DoctorInvitation',1,'Invite a doctor',@ds,'Invite a doctor', newid(), CURRENT_TIMESTAMP),
('DoctorVisit',2,'Visit a doctor',@ds,'Visit a doctor', newid(), CURRENT_TIMESTAMP)
