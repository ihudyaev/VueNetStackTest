--
-- PostgreSQL database dump
--

-- Dumped from database version 12.2
-- Dumped by pg_dump version 12.2

-- Started on 2020-04-27 01:09:50

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 212 (class 1255 OID 24590)
-- Name: trigger_set_updated_timestamp(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.trigger_set_updated_timestamp() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  NEW.updated_at = NOW();
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.trigger_set_updated_timestamp() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 207 (class 1259 OID 16422)
-- Name: Category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Category" (
    id integer NOT NULL,
    title text NOT NULL,
    description text NOT NULL
);


ALTER TABLE public."Category" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16420)
-- Name: Category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Category" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Category_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 205 (class 1259 OID 16406)
-- Name: Role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Role" (
    id integer NOT NULL,
    rolename text NOT NULL
);


ALTER TABLE public."Role" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16404)
-- Name: Role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Role" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Role_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 209 (class 1259 OID 16432)
-- Name: Topic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Topic" (
    id integer NOT NULL,
    category_id integer NOT NULL,
    title text NOT NULL,
    user_id integer NOT NULL,
    description text NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    user_displayname text
);


ALTER TABLE public."Topic" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16442)
-- Name: Topic_Reply; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Topic_Reply" (
    id integer NOT NULL,
    topic_id integer NOT NULL,
    user_id integer NOT NULL,
    description text NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    user_displayname text
);


ALTER TABLE public."Topic_Reply" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 16440)
-- Name: Topic_Reply_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Topic_Reply" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Topic_Reply_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 208 (class 1259 OID 16430)
-- Name: Topic_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Topic" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Topic_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 202 (class 1259 OID 16394)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    id integer NOT NULL,
    display_name text NOT NULL,
    email text NOT NULL,
    disabled boolean NOT NULL,
    role_id integer NOT NULL,
    "Password" text,
    user_name text NOT NULL,
    "Birthdate" date,
    "Name" text,
    "Surname" text
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16402)
-- Name: User_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."User" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."User_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 2731 (class 2606 OID 16429)
-- Name: Category Category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY (id);


--
-- TOC entry 2729 (class 2606 OID 16413)
-- Name: Role Role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "Role_pkey" PRIMARY KEY (id);


--
-- TOC entry 2737 (class 2606 OID 16449)
-- Name: Topic_Reply Topic_Reply_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic_Reply"
    ADD CONSTRAINT "Topic_Reply_pkey" PRIMARY KEY (id);


--
-- TOC entry 2733 (class 2606 OID 16439)
-- Name: Topic Topic_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic"
    ADD CONSTRAINT "Topic_pkey" PRIMARY KEY (id);


--
-- TOC entry 2722 (class 2606 OID 16401)
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);


--
-- TOC entry 2725 (class 2606 OID 32780)
-- Name: User unique_email; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT unique_email UNIQUE (email);


--
-- TOC entry 2727 (class 2606 OID 32782)
-- Name: User unique_username; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT unique_username UNIQUE (user_name);


--
-- TOC entry 2738 (class 1259 OID 16535)
-- Name: fki_TopiReply_User_FK; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_TopiReply_User_FK" ON public."Topic_Reply" USING btree (user_id);


--
-- TOC entry 2739 (class 1259 OID 16519)
-- Name: fki_TopicReply_Topic_FK; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_TopicReply_Topic_FK" ON public."Topic_Reply" USING btree (topic_id);


--
-- TOC entry 2734 (class 1259 OID 16504)
-- Name: fki_Topic_Category_FK; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_Topic_Category_FK" ON public."Topic" USING btree (category_id);


--
-- TOC entry 2735 (class 1259 OID 16489)
-- Name: fki_Topic_user_FK; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_Topic_user_FK" ON public."Topic" USING btree (user_id);


--
-- TOC entry 2723 (class 1259 OID 16475)
-- Name: fki_User_role; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_User_role" ON public."User" USING btree (role_id);


--
-- TOC entry 2745 (class 2620 OID 24591)
-- Name: Topic_Reply set_timestamp; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public."Topic_Reply" FOR EACH ROW EXECUTE FUNCTION public.trigger_set_updated_timestamp();


--
-- TOC entry 2744 (class 2606 OID 16536)
-- Name: Topic_Reply TopiReply_User_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic_Reply"
    ADD CONSTRAINT "TopiReply_User_FK" FOREIGN KEY (user_id) REFERENCES public."User"(id) NOT VALID;


--
-- TOC entry 2743 (class 2606 OID 16520)
-- Name: Topic_Reply TopicReply_Topic_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic_Reply"
    ADD CONSTRAINT "TopicReply_Topic_FK" FOREIGN KEY (topic_id) REFERENCES public."Topic"(id) NOT VALID;


--
-- TOC entry 2742 (class 2606 OID 16505)
-- Name: Topic Topic_Category_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic"
    ADD CONSTRAINT "Topic_Category_FK" FOREIGN KEY (category_id) REFERENCES public."Category"(id) NOT VALID;


--
-- TOC entry 2741 (class 2606 OID 16490)
-- Name: Topic Topic_user_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Topic"
    ADD CONSTRAINT "Topic_user_FK" FOREIGN KEY (user_id) REFERENCES public."User"(id) NOT VALID;


--
-- TOC entry 2740 (class 2606 OID 16476)
-- Name: User User_role; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_role" FOREIGN KEY (role_id) REFERENCES public."Role"(id) NOT VALID;


-- Completed on 2020-04-27 01:09:50

--
-- PostgreSQL database dump complete
--

