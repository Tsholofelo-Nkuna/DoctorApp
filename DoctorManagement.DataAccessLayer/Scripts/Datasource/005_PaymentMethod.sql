declare @ds nvarchar(max) = 'PaymentMethod';
insert into DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('Card',1,'Card',@ds,'Card', newid(), CURRENT_TIMESTAMP),
('Cash',2,'Cash',@ds,'Cash', newid(), CURRENT_TIMESTAMP),
('MedicalAid',3,'Medical aid',@ds,'MedicalAid', newId(),CURRENT_TIMESTAMP)


