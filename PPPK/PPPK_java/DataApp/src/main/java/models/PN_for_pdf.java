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
public class PN_for_pdf {
    private String datum_izrade;
    private String datum_pocetka;
    private String datum_zavrsetka;
    private String status;
    private String ime;
    private String prezime;
    private String marka;
    private String godina_proizvodnje;

    public PN_for_pdf(String datum_izrade, String datum_pocetka, String datum_zavrsetka, String status, String ime, String prezime, String marka, String godina_proizvodnje) {
        this.datum_izrade = datum_izrade;
        this.datum_pocetka = datum_pocetka;
        this.datum_zavrsetka = datum_zavrsetka;
        this.status = status;
        this.ime = ime;
        this.prezime = prezime;
        this.marka = marka;
        this.godina_proizvodnje = godina_proizvodnje;
    }
    
    
    public String getDatum_izrade() {
        return datum_izrade;
    }

    public void setDatum_izrade(String datum_izrade) {
        this.datum_izrade = datum_izrade;
    }

    public String getDatum_pocetka() {
        return datum_pocetka;
    }

    public void setDatum_pocetka(String datum_pocetka) {
        this.datum_pocetka = datum_pocetka;
    }

    public String getDatum_zavrsetka() {
        return datum_zavrsetka;
    }

    public void setDatum_zavrsetka(String datum_zavrsetka) {
        this.datum_zavrsetka = datum_zavrsetka;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
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

    public String getMarka() {
        return marka;
    }

    public void setMarka(String marka) {
        this.marka = marka;
    }

    public String getGodina_proizvodnje() {
        return godina_proizvodnje;
    }

    public void setGodina_proizvodnje(String godina_proizvodnje) {
        this.godina_proizvodnje = godina_proizvodnje;
    }
    
}
