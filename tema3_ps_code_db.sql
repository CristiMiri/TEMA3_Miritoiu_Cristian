DROP TABLE IF EXISTS prezentari;
DROP TABLE IF EXISTS utilizatori;
DROP TABLE IF EXISTS conferinte;
DROP TABLE IF EXISTS participanti;

-- Create the Conferinte table
CREATE TABLE Conferinte (
    id SERIAL PRIMARY KEY,
    titlu VARCHAR(100),
    locatie VARCHAR(100),
    data DATE
);

-- Create the table participanti first, because it's now referenced in Prezentari
CREATE TABLE participanti (
    id SERIAL PRIMARY KEY,
    nume VARCHAR(255),
    email VARCHAR(255),
    telefon VARCHAR(20),
    cnp VARCHAR(13) UNIQUE, -- Assuming CNP has a fixed length, adjust according to your needs
    pdf_file_path VARCHAR(255), -- Path to the uploaded PDF file
    UNIQUE(email) -- Ensures that email addresses are unique
);

-- Create the Prezentari table with a reference to participanti
CREATE TABLE Prezentari (
    id SERIAL PRIMARY KEY,
    titlu VARCHAR(100),
    id_autor INTEGER REFERENCES participanti(id), -- Reference to the participanti table
    descriere TEXT,
    data DATE,
    ora TIME,
    sectiune VARCHAR(50), -- enum values
    id_conferinta INTEGER REFERENCES Conferinte(id) -- Reference to the Conferinte table
);

-- Create the table utilizatori
CREATE TABLE utilizatori (
    id SERIAL PRIMARY KEY,
    nume VARCHAR(255),
    email VARCHAR(255) UNIQUE,
    parola VARCHAR(255),
    user_type VARCHAR(50),
    telefon VARCHAR(20)
);


-- Insert dummy values into Conferinte table
INSERT INTO Conferinte (titlu, locatie, data) VALUES 
('AI Conference', 'New York', '2024-04-10'),
('Web Development Summit', 'San Francisco', '2024-04-15'),
('Medical Research Symposium', 'Chicago', '2024-05-05');


-- Insert dummy values into the participanti table for the authors
INSERT INTO participanti (nume, email, telefon, cnp, pdf_file_path) VALUES 
('Alex Green', 'alex.green@example.com', '555-1234', '1234567890123', '/pdfs/alex-green-cv.pdf'),
('Samantha Blue', 's.blue@example.com', '555-5678', '1234567890124', '/pdfs/samantha-blue-portfolio.pdf'),
('Chris Yellow', 'chris.yellow@example.com', '555-9012', '1234567890125', '/pdfs/chris-yellow-research.pdf'),
('Patricia White', 'patricia.white@example.com', '555-3456', '1234567890126', '/pdfs/patricia-white-paper.pdf'),
('Daniel Brown', 'd.brown@example.com', '555-7890', '1234567890127', '/pdfs/daniel-brown-resume.pdf'),
('Anca Iordan', 'anca.iordan@cs.utcluj.ro', '555-5678', '9876543210987', '/pdfs/anca-iordan-cv.pdf');

-- Insert dummy values into Prezentari table using participant IDs as authors
INSERT INTO Prezentari (titlu, id_autor, descriere, data, ora, sectiune, id_conferinta) VALUES 
('Introduction to Quantum Computing', 1, 'An overview of quantum computing principles', '2024-04-10', '09:00', 'STIINTE', 1),
('Web Development Trends', 2, 'Exploring the latest trends in web development', '2024-04-11', '10:30', 'TEHNOLOGIE', 1),
('Advancements in Cancer Treatment', 3, 'Recent breakthroughs in cancer treatment', '2024-04-12', '13:45', 'MEDICINA', 2),
('Modern Art and Its Impact', 4, 'Analyzing contemporary art movements', '2024-04-13', '15:00', 'ARTA', 2),
('The Science of Athletic Performance', 5, 'Understanding the physiology behind sports performance', '2024-04-14', '11:00', 'SPORT', 3);

-- Insert dummy values into the table utilizatori
INSERT INTO utilizatori (nume, email, parola, user_type, telefon)
VALUES
    ('John Doe', 'john@example.com', 'password123', 'PARTICIPANT', '123456789'),
    ('Jane Smith', 'jane@example.com', 'password456', 'ORGANIZATOR', '987654321'),
    ('Admin User', 'admin@example.com', 'adminpassword', 'ADMINISTRATOR', '555555555');

