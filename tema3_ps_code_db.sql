-- Drop existing tables in reverse order of dependencies to avoid conflicts
DROP TABLE IF EXISTS presentation_Participant;
DROP TABLE IF EXISTS presentation;
DROP TABLE IF EXISTS participant;
DROP TABLE IF EXISTS userAccount;
DROP TABLE IF EXISTS conference;

-- Create the Conference table
CREATE TABLE conference (
    id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    location VARCHAR(100) NOT NULL,
    date DATE NOT NULL
);

-- Create the Participant table 
CREATE TABLE participant (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    phone VARCHAR(20),
    cnp VARCHAR(13) UNIQUE, -- Adjust according to data protection laws
    pdf_file_path VARCHAR(255) NOT NULL,
    photo_file_path VARCHAR(255) -- Path to the uploaded photo file
);

-- Create the userAccount table
CREATE TABLE userAccount (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    user_type VARCHAR(50) NOT NULL CHECK (user_type IN ('PARTICIPANT', 'ORGANIZER', 'ADMINISTRATOR')),
    phone VARCHAR(20)
);

-- Create the Presentation table 
CREATE TABLE presentation (
    id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
    date DATE NOT NULL,
    hour TIME NOT NULL,
    section VARCHAR(50) NOT NULL,
    id_conference INTEGER NOT NULL REFERENCES conference(id),
    id_author INTEGER NOT NULL REFERENCES participant(id)
);

-- Create the presentation_Participant table
CREATE TABLE presentation_Participant (
    id_presentation INTEGER NOT NULL REFERENCES presentation(id),
    id_participant INTEGER NOT NULL REFERENCES participant(id),
    PRIMARY KEY (id_presentation, id_participant)
);

-- Insert values into conference
INSERT INTO conference (title, location, date) VALUES 
('AI Conference', 'New York', '2024-04-10'),
('Web Development Summit', 'San Francisco', '2024-04-15'),
('Medical Research Symposium', 'Chicago', '2024-05-05');

-- Insert values into participant
INSERT INTO participant (name, email, phone, cnp, pdf_file_path, photo_file_path) VALUES 
('Alex Green', 'alex.green@example.com', '555-1234', '1234567890123', 'alex-green-cv.pdf', 'alex-green-photo.jpg'),
('Samantha Blue', 's.blue@example.com', '555-5678', '1234567890124', 'samantha-blue-portfolio.pdf', 'samantha-blue-photo.jpg'),
('Chris Yellow', 'chris.yellow@example.com', '555-9012', '1234567890125', 'chris-yellow-research.pdf', 'chris-yellow-photo.jpg'),
('Patricia White', 'patricia.white@example.com', '555-3456', '1234567890126', 'patricia-white-paper.pdf', 'patricia-white-photo.jpg'),
('Daniel Brown', 'd.brown@example.com', '555-7890', '1234567890127', 'daniel-brown-resume.pdf', 'daniel-brown-photo.jpg'),
('Anca Iordan', 'anca.iordan@cs.utcluj.ro', '555-5678', '9876543210987', 'anca-iordan-cv.pdf', 'anca-iordan-photo.jpg');

-- Insert values into userAccount
INSERT INTO userAccount (name, email, password, user_type, phone) VALUES
('John Doe', 'john@example.com', 'password123', 'PARTICIPANT', '123456789'),
('Jane Smith', 'jane@example.com', 'password456', 'ORGANIZER', '987654321'),
('Admin User', 'admin@example.com', 'adminpassword', 'ADMINISTRATOR', '555555555');

-- Insert values into presentation
INSERT INTO presentation (title, description, date, hour, section, id_conference, id_author) VALUES 
('Introduction to Quantum Computing', 'An overview of quantum computing principles', '2024-04-10', '09:00', 'SCIENCE', 1, 1),
('Web Development Trends','Exploring the latest trends in web development', '2024-04-11', '10:30', 'TECHNOLOGY', 1, 1),
('Advancements in Cancer Treatment', 'Recent breakthroughs in cancer treatment', '2024-04-12', '13:45', 'MEDICINE', 2, 2),
('Modern Art and Its Impact','Analyzing contemporary art movements', '2024-04-13', '15:00', 'ART', 2, 2),
('The Science of Athletic Performance','Understanding the physiology behind sports performance', '2024-04-14', '11:00', 'SPORT', 3, 3);

-- Insert values into presentation_Participant
INSERT INTO presentation_Participant (id_presentation, id_participant) VALUES
(1, 1),
(2, 2),
(3, 3),
(1, 2),
(2, 1),
(3, 4);
