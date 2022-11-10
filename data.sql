create table devices
(
    id            int auto_increment
        primary key,
    computer_type enum ('desktop', 'laptop', 'tablet', 'phone', 'unspecified') default 'unspecified' not null,
    vendor        varchar(64)                                                                        not null,
    model         varchar(64)                                                                        not null,
    price         double                                                                             not null,
    link          varchar(256)                                                                       not null,
    description   varchar(1024)                                                                      null,
    specs         varchar(1024)                                                                      null,
    constraint id
        unique (id)
);

-- admin account
CREATE USER 'admin'@'localhost'
    IDENTIFIED BY 'its462-admin-super-good-password';

-- end user account
CREATE USER 'end_user'@'localhost'
    IDENTIFIED BY 'its462-user-password';

-- *** *** *** *** *** END USER COMMANDS *** *** *** *** ***
-- returns a list of all the computer models and their pk
DELIMITER //
CREATE PROCEDURE list_devices ()
    BEGIN
        SELECT id, model, price FROM devices;
    END //

-- returns details on a specific device
DELIMITER //
CREATE PROCEDURE get_device_details(IN given_id int)
    BEGIN
        SELECT * FROM devices WHERE id=given_id;
    end //

-- list all devices by a given filter
DELIMITER //
CREATE PROCEDURE list_filtered_devices(IN filter_column varchar(16), IN filter_chosen varchar(128))
    BEGIN
        SET @stmt=CONCAT('SELECT id, model, price FROM devices WHERE ', filter_column, '=',filter_chosen);
        SELECT id, model, price FROM devices WHERE filter_column=filter_chosen;
        PREPARE stmt_prepared FROM @stmt;
        EXECUTE stmt_prepared;
        DEALLOCATE PREPARE stmt_prepared;
    end //

-- get all the columns, this is used for filters
DELIMITER //
CREATE PROCEDURE list_filters()
    BEGIN
        SELECT COLUMN_NAME AS filter FROM information_schema.COLUMNS
            WHERE TABLE_SCHEMA='project' AND TABLE_NAME='devices';
    end //

-- *** *** *** *** *** ADMIN COMMANDS *** *** *** *** ***
-- add a new entry
DELIMITER //
CREATE PROCEDURE add_device(
    IN given_type enum('desktop', 'laptop', 'phone', 'unspecified'),
    IN given_vendor varchar(64), IN given_model varchar(64),
    IN given_price decimal(10,2), IN given_link varchar(256),
    IN given_description varchar(1024), IN given_specs varchar(1024)
)
    BEGIN
        INSERT INTO project.devices (computer_type, vendor, model, price, link, description, specs)
            VALUES (given_type, given_vendor, given_model, given_price, given_link, given_description, given_specs);
    end //

-- grant the proper privs
GRANT EXECUTE ON PROCEDURE list_devices TO 'end_user'@'localhost';
GRANT EXECUTE ON PROCEDURE get_device_details TO 'end_user'@'localhost';
GRANT EXECUTE ON PROCEDURE list_filtered_devices TO 'end_user'@'localhost';
GRANT EXECUTE ON PROCEDURE list_filters TO 'end_user'@'localhost';

GRANT EXECUTE ON PROCEDURE add_device TO 'admin'@'localhost';