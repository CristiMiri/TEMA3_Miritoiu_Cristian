-- Start transaction
BEGIN;

-- Drop existing tables in reverse order of dependencies to avoid conflicts
DROP TABLE IF EXISTS Prezentare_Participant;
DROP TABLE IF EXISTS Prezentare;
DROP TABLE IF EXISTS Participant;
DROP TABLE IF EXISTS Utilizator;
DROP TABLE IF EXISTS Conferinta;

-- Create the Conferinta table
CREATE TABLE conferinta (
    id SERIAL PRIMARY KEY,
    titlu VARCHAR(100) NOT NULL,
    locatie VARCHAR(100) NOT NULL,
    data DATE NOT NULL
);

-- Create the Participant table 
CREATE TABLE participant (
    id SERIAL PRIMARY KEY,
    nume VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    telefon VARCHAR(20),
    cnp VARCHAR(13) UNIQUE, -- Adjust according to data protection laws
    pdf_file_path VARCHAR(255) NOT NULL,
    photo_file_path VARCHAR(255) -- Path to the uploaded photo file
);

-- Create the Utilizator table
CREATE TABLE utilizator (
    id SERIAL PRIMARY KEY,
    nume VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    parola VARCHAR(255) NOT NULL,
    user_type VARCHAR(50) NOT NULL CHECK (user_type IN ('PARTICIPANT', 'ORGANIZATOR', 'ADMINISTRATOR')),
    telefon VARCHAR(20)
);


-- Create the Prezentare table with references
CREATE TABLE prezentare (
    id SERIAL PRIMARY KEY,
    titlu VARCHAR(100) NOT NULL,
    descriere TEXT NOT NULL,
    data DATE NOT NULL,
    ora TIME NOT NULL,
    sectiune VARCHAR(50) NOT NULL,
    id_conferinta INTEGER NOT NULL REFERENCES conferinta(id),
	id_autor INTEGER NOT NULL REFERENCES participant(id)
);

-- Create the Prezentare_Participant table
CREATE TABLE prezentare_participant (
    id_prezentare INTEGER NOT NULL REFERENCES prezentare(id),
    id_participant INTEGER NOT NULL REFERENCES participant(id)       
);

-- Commit transaction
COMMIT;

BEGIN;
-- Insert values into Conferinte
INSERT INTO Conferinta (titlu, locatie, data) VALUES 
('AI Conference', 'New York', '2024-04-10'),
('Web Development Summit', 'San Francisco', '2024-04-15'),
('Medical Research Symposium', 'Chicago', '2024-05-05');

-- Insert values into participanti
INSERT INTO Participant (nume, email, telefon, cnp, pdf_file_path, photo_file_path) VALUES 
('Alex Green', 'alex.green@example.com', '555-1234', '1234567890123', '/pdfs/alex-green-cv.pdf', '/images/alex-green-photo.jpg'),
('Samantha Blue', 's.blue@example.com', '555-5678', '1234567890124', '/pdfs/samantha-blue-portfolio.pdf', '/images/samantha-blue-photo.jpg'),
('Chris Yellow', 'chris.yellow@example.com', '555-9012', '1234567890125', '/pdfs/chris-yellow-research.pdf', '/images/chris-yellow-photo.jpg'),
('Patricia White', 'patricia.white@example.com', '555-3456', '1234567890126', '/pdfs/patricia-white-paper.pdf', '/images/patricia-white-photo.jpg'),
('Daniel Brown', 'd.brown@example.com', '555-7890', '1234567890127', '/pdfs/daniel-brown-resume.pdf', '/images/daniel-brown-photo.jpg'),
('Anca Iordan', 'anca.iordan@cs.utcluj.ro', '555-5678', '9876543210987', '/pdfs/anca-iordan-cv.pdf', '/images/anca-iordan-photo.jpg');

-- Insert values into utilizatori
INSERT INTO Utilizator (nume, email, parola, user_type, telefon) VALUES
('John Doe', 'john@example.com', 'password123', 'PARTICIPANT', '123456789'),
('Jane Smith', 'jane@example.com', 'password456', 'ORGANIZATOR', '987654321'),
('Admin User', 'admin@example.com', 'adminpassword', 'ADMINISTRATOR', '555555555');

-- Insert values into Prezentari
INSERT INTO Prezentare (titlu, descriere, data, ora, sectiune, id_conferinta,id_autor) VALUES 
('Introduction to Quantum Computing', 'An overview of quantum computing principles', '2024-04-10', '09:00', 'STIINTE', 1,1),
('Web Development Trends','Exploring the latest trends in web development', '2024-04-11', '10:30', 'TEHNOLOGIE', 1,1),
('Advancements in Cancer Treatment', 'Recent breakthroughs in cancer treatment', '2024-04-12', '13:45', 'MEDICINA', 2,2),
('Modern Art and Its Impact','Analyzing contemporary art movements', '2024-04-13', '15:00', 'ARTA', 2,2),
('The Science of Athletic Performance','Understanding the physiology behind sports performance', '2024-04-14', '11:00', 'SPORT', 3,3);

-- Insert values into Prezentare_Participanti
INSERT INTO Prezentare_Participant (id_prezentare, id_participant) VALUES
(1, 1 ),
(2, 2 ),
(3, 3 ),
(1, 2 ),
(2, 1 ),
(3, 4);
COMMIT;
