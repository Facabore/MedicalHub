CREATE TABLE Patients (
PatientID INT NOT NULL IDENTITY(1,1),
FirstName VARCHAR(100),
LastName VARCHAR(100),
IdentificationNumber VARCHAR(50),
Email VARCHAR(100),
BirthDate DATE,
PRIMARY KEY (PatientID),
CONSTRAINT unique_identification_patient UNIQUE (IdentificationNumber)
);





CREATE TABLE Doctors (
DoctorID INT NOT NULL IDENTITY(1,1),
FirstName VARCHAR(100),
LastName VARCHAR(100),
IdentificationNumber VARCHAR(50),
Email VARCHAR(100),
PRIMARY KEY (DoctorID),
CONSTRAINT unique_identification_doctor UNIQUE (IdentificationNumber)
);

CREATE TABLE Consultations (
ConsultationID INT NOT NULL IDENTITY(1,1),
PatientIdentificationNumber VARCHAR(50),
DoctorIdentificationNumber VARCHAR(50),
ConsultationDate DATE DEFAULT GETDATE(),
ReasonForConsultation  VARCHAR(255),
Diagnosis VARCHAR(255),
Status VARCHAR(20) DEFAULT 'Pendiente',
Results VARCHAR(255) DEFAULT NULL,
PendingOrders VARCHAR(255) DEFAULT NULL,
PRIMARY KEY (ConsultationID),
CONSTRAINT fk_patient_identification FOREIGN KEY (PatientIdentificationNumber) REFERENCES Patients(IdentificationNumber),
CONSTRAINT fk_doctor_identification FOREIGN KEY (DoctorIdentificationNumber) REFERENCES Doctors(IdentificationNumber)
);


-- DATA INSERTION

-- Patients insert data
INSERT INTO Patients (FirstName, LastName, IdentificationNumber, Email, BirthDate) VALUES
('John', 'Doe', '123456789', 'JhonDoe@gmail.com', '1980-01-01'),
('Jane', 'Smith', '987654321','' ,'2010-05-12'),
('Alice', 'Brown', '456789123','' ,'2005-07-30'),
('Bob', 'Davis', '789123456', '','2012-11-22'),
('Steve', 'Garris', '10587503', '','2005-12-12');

-- Doctors insert data
INSERT INTO Doctors (FirstName, LastName, IdentificationNumber, Specialty) VALUES
('James', 'Wilson', '111222333', 'James@gmail.com'),
('Lisa', 'Cuddy', '444555666', 'Lisa@gmail.com'),
('Gregory', 'House', '777888999', 'Gregory@gmail.com'),
('Robert', 'Chase', '123321123', 'Rober@gmail.com'),
('Allison', 'Cameron', '456654456', 'Allison@gmail.com');

-- Consultations insert data
INSERT INTO Consultations (PatientIdentificationNumber, DoctorIdentificationNumber, ConsultationDate, ReasonForConsultation, Diagnosis, Status, Results, PendingOrders)
VALUES
('123456789', '456654456', '2023-01-01', 'Routine check-up, high blood pressure', 'Hypertension', 'Complet', 'Antihypertensive treatment prescribed', 'Follow-up in 3 months'),
('987654321', '444555666', '2023-01-15', 'Persistent fatigue and weight loss', 'Cancer diagnosis', 'Pendiente', NULL, 'CT scan scheduled'),
('789123456', '111222333', '2023-01-20', 'Fall from stairs', 'Fracture', 'Reprogrammed', NULL, 'X-ray rescheduled for next week'),
('456789123', '777888999', '2024-10-25', 'Follow-up for healing process', 'Fracture', 'Complet', 'Fracture healed, patient discharged', 'Physiotherapy sessions recommended');

-- QUERYS
-- INNER JOIN
-- SHow patients and doctor, and her diagnosis together
SELECT
    CONCAT(Patients.FirstName, ' ', Patients.LastName) AS FullName,
    CONCAT(Doctors.FirstName, ' ', Doctors.LastName) AS DoctorFullName,
    Consultations.Diagnosis
FROM Consultations
     INNER JOIN Patients ON Consultations.PatientIdentificationNumber = Patients.IdentificationNumber
     INNER JOIN Doctors ON Consultations.DoctorIdentificationNumber = Doctors.IdentificationNumber;

-- Show status consultation
SELECT
    CONCAT(Patients.FirstName, ' ', Patients.LastName) AS FullName,
    Consultations.ReasonForConsultation,
    Consultations.ConsultationDate,
    Consultations.Status
FROM Consultations
     INNER JOIN Patients ON Consultations.PatientIdentificationNumber  = Patients.IdentificationNumber
-- LEFT JOIN
-- Show all Patients and her Diagnosis 
SELECT
    CONCAT(Patients.FirstName,' ',Patients.LastName) AS FullName,
    Consultations.Diagnosis,
    Consultations.ConsultationDate
FROM Patients
     LEFT JOIN Consultations ON Patients.IdentificationNumber = Consultations.PatientIdentificationNumber;

-- UNION
-- Combine Patients and Doctors as Role in a Table
SELECT
    CONCAT(Patients.FirstName,' ',Patients.LastName) AS FullName,
    'Patient' AS Role
FROM Patients
UNION
SELECT
    CONCAT(Doctors.FirstName,' ',Doctors.LastName) AS FullName,
    'Doctor' AS Role
FROM Doctors;

-- CASE
-- Show the most serious Diagnosis
SELECT
    CONCAT(Patients.FirstName,' ',Patients.LastName) AS Patient,
    CONCAT(Doctors.FirstName,' ',Doctors.LastName) AS Doctor,
    Consultations.Diagnosis,
    CASE
        WHEN Consultations.Diagnosis = 'Hypertension' THEN 'serious'
        WHEN Consultations.Diagnosis = 'Cancer diagnosis' THEN 'serious'
        ELSE 'mild'
        END AS Severity
FROM Consultations
         INNER JOIN Patients ON Consultations.PatientIdentificationNumber = Patients.IdentificationNumber
         INNER JOIN Doctors ON Consultations.DoctorIdentificationNumber = Doctors.IdentificationNumber ;
