CREATE SCHEMA question_board

CREATE TABLE question_board.question
(
    id serial,
	board_id int,
    title varchar,
    user_id  int,
	description varchar,
	create_date TIMESTAMP,
	update_date TIMESTAMP,
	answer varchar,
	answer_user_id int,
	answer_create_date TIMESTAMP,
	answer_update_date TIMESTAMP,
    PRIMARY KEY (id)
);
CREATE TABLE question_board.board
(
    id serial,
    title varchar,
	user_id int,
    PRIMARY KEY (id)
);
CREATE TABLE question_board.user
(
	id serial,
    username  varchar,
    password varchar,
	type int,
    PRIMARY KEY (id)
);
CREATE TABLE question_board.user_access
(
	user_id int,
	board_id int,
	PRIMARY KEY (user_id,board_id)
);

ALTER TABLE question_board.question
    ADD FOREIGN KEY (board_id) REFERENCES question_board.board (id),
	ADD FOREIGN KEY (user_id) REFERENCES question_board.user (id),
	ADD FOREIGN KEY (answer_user_id) REFERENCES question_board.user (id);

ALTER TABLE question_board.board
    ADD FOREIGN KEY (user_id) REFERENCES question_board.user (id);
	
ALTER TABLE question_board.user_access
    ADD FOREIGN KEY (board_id) REFERENCES question_board.board (id),
	ADD FOREIGN KEY (user_id) REFERENCES question_board.user (id);
