create table computers
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

