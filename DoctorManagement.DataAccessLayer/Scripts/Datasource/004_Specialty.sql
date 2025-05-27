declare @ds nvarchar(max) = 'Specialty';
insert into DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('Doctor',1,'Dr(GP & Other Specialization)',@ds,'Doctor', newid(), CURRENT_TIMESTAMP),
('Dental',2,'Dental',@ds,'Dental', newid(), CURRENT_TIMESTAMP),
('Audiology',3,'Audiology',@ds,'Audiology', newId(),CURRENT_TIMESTAMP),
('Optometry',4,'Optometry',@ds,'Optometry', newId(), CURRENT_TIMESTAMP),
('Nurse',5,'Registered Nurse',@ds,'Nurse', newId(), CURRENT_TIMESTAMP),
('Dermatology',6,'Dermatology',@ds,'Dermatology', newId(), CURRENT_TIMESTAMP),
('Physiology',7,'Physiology',@ds,'Physiology', newId(), CURRENT_TIMESTAMP)

