PGDMP                         y            BDDProyecto3    13.0    13.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16397    BDDProyecto3    DATABASE     j   CREATE DATABASE "BDDProyecto3" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "BDDProyecto3";
                postgres    false            �            1259    16398    bddclientes    TABLE     y  CREATE TABLE public.bddclientes (
    nit character varying(10) NOT NULL,
    dpi character varying(15),
    nombre character varying(40),
    fechanac character varying(10),
    genero character varying(10) NOT NULL,
    estadocivil character varying(10) NOT NULL,
    nombreempresa character varying(50),
    contactoempresa character varying(10),
    tipocliente integer
);
    DROP TABLE public.bddclientes;
       public         heap    postgres    false            �            1259    16401    bddproductos    TABLE     �   CREATE TABLE public.bddproductos (
    nombreproducto character varying(40) NOT NULL,
    marca character varying(20) NOT NULL,
    categoria character varying(50) NOT NULL,
    precio integer NOT NULL
);
     DROP TABLE public.bddproductos;
       public         heap    postgres    false            �            1259    16404    pruebas    TABLE        CREATE TABLE public.pruebas (
    id integer NOT NULL,
    nombre character varying(10),
    apellido character varying(10)
);
    DROP TABLE public.pruebas;
       public         heap    postgres    false           