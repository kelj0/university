/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package models;

/**
 *
 * @author keljo
 */
public class Vozac {

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getIme() {
        return ime;
    }

    public void setIme(String ime) {
        this.ime = ime;
    }

    public String getPrezime() {
        return prezime;
    }

    public void setPrezime(String prezime) {
        this.prezime = prezime;
    }

    public String getBroj_mobitela() {
        return broj_mobitela;
    }

    public void setBroj_mobitela(String broj_mobitela) {
        this.broj_mobitela = broj_mobitela;
    }

    public String getBroj_vozacke() {
        return broj_vozacke;
    }

    public void setBroj_vozacke(String broj_vozacke) {
        this.broj_vozacke = broj_vozacke;
    }
    private int id;
    private String ime;
    private String prezime;
    private String broj_mobitela;
    private String broj_vozacke;    

    public Vozac(int id, String ime, String prezime, String broj_mobitela, String broj_vozacke) {
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.broj_mobitela = broj_mobitela;
        this.broj_vozacke = broj_vozacke;
    }
}
