create table rfid_info (rfid_no varchar2(20) primary key, p_number varchar2(20), flag_no number);

create table package_info (p_number varchar2(20) primary key, s_c_name varchar2(30), s_c_add varchar2(60), d_c_name varchar2(30), d_c_address varchar2(60), p_desc varchar2(60));

create table truck_geo (truck_no varchar(20), latitude varchar(10), longitude varchar(10));

create table truck_track (truck_no varchar(20), tracking_no varchar(20));

create table track_info (tracking_no varchar2(20), p_number varchar2(20));

select * from truck_track

drop table truck_geo
drop table truck_track