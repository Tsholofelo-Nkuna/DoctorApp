declare @ds nvarchar(max) = 'ProfessionalTitle';
insert DataSource([Name],[Value],[Description], TypeCode, Abbr,Id, CreatedOn)
Values 
('Doctor',1,'Dr.',@ds,'Dr', newid(), CURRENT_TIMESTAMP),
('Honorable',2,'Hon.',@ds,'Hon', newid(), CURRENT_TIMESTAMP),
('Mister',3,'Mr.',@ds,'Dr', newId(),CURRENT_TIMESTAMP),
('Mistress',4,'Mrs.',@ds,'Mrs', newId(), CURRENT_TIMESTAMP),
('Miss',5,'Ms.',@ds,'Ms', newId(), CURRENT_TIMESTAMP)