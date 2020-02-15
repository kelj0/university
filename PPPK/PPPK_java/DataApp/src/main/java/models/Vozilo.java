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
public class Vozilo {
    private int id;
    private int tip_vozila_id;
    private String marka;
    private int godina_proizvodnje;
    private double pocetni_km;
    private double trenutni_km;
    
    
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getTip_vozila_id() {
        return tip_vozila_id;
    }

    public void setTip_vozila_id(int tip_vozila_id) {
        this.tip_vozila_id = tip_vozila_id;
    }

    public String getMarka() {
        return marka;
    }

    public void setMarka(String marka) {
        this.marka = marka;
    }

    public int getGodina_proizvodnje() {
        return godina_proizvodnje;
    }

    public void setGodina_proizvodnje(int godina_proizvodnje) {
        this.godina_proizvodnje = godina_proizvodnje;
    }

    public double getPocetni_km() {
        return pocetni_km;
    }

    public void setPocetni_km(double pocetni_km) {
        this.pocetni_km = pocetni_km;
    }

    public double getTrenutni_km() {
        return trenutni_km;
    }

    public void setTrenutni_km(double trenutni_km) {
        this.trenutni_km = trenutni_km;
    }
    
    public Vozilo(int id, int tip_vozila_id, String marka, int godina_proizvodnje, double pocetni_km, double trenutni_km) {
        this.id = id;
        this.tip_vozila_id = tip_vozila_id;
        this.marka = marka;
        this.godina_proizvodnje = godina_proizvodnje;
        this.pocetni_km = pocetni_km;
        this.trenutni_km = trenutni_km;
    }
    
    public Vozilo(int tip_vozila_id, String marka, int godina_proizvodnje, double pocetni_km, double trenutni_km) {
        this.tip_vozila_id = tip_vozila_id;
        this.marka = marka;
        this.godina_proizvodnje = godina_proizvodnje;
        this.pocetni_km = pocetni_km;
        this.trenutni_km = trenutni_km;
    }   
}
