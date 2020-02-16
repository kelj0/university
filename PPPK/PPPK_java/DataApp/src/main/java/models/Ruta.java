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
public class Ruta {
    private int id;
    private double x_koordinata_a;
    private double y_koordinata_a;
    private double x_koordinata_b;
    private double y_koordinata_b;
    private double km_izmedu_a_b;
    private double prosjecna_brzina;

    public Ruta(int id, double x_koordinata_a, double y_koordinata_a, double x_koordinata_b, double y_koordinata_b, double km_izmedu_a_b, double prosjecna_brzina) {
        this.id = id;
        this.x_koordinata_a = x_koordinata_a;
        this.y_koordinata_a = y_koordinata_a;
        this.x_koordinata_b = x_koordinata_b;
        this.y_koordinata_b = y_koordinata_b;
        this.km_izmedu_a_b = km_izmedu_a_b;
        this.prosjecna_brzina = prosjecna_brzina;
    }
    
    public Ruta(double x_koordinata_a, double y_koordinata_a, double x_koordinata_b, double y_koordinata_b, double km_izmedu_a_b, double prosjecna_brzina) {
        this.x_koordinata_a = x_koordinata_a;
        this.y_koordinata_a = y_koordinata_a;
        this.x_koordinata_b = x_koordinata_b;
        this.y_koordinata_b = y_koordinata_b;
        this.km_izmedu_a_b = km_izmedu_a_b;
        this.prosjecna_brzina = prosjecna_brzina;
    }
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public double getX_koordinata_a() {
        return x_koordinata_a;
    }

    public void setX_koordinata_a(double x_koordinata_a) {
        this.x_koordinata_a = x_koordinata_a;
    }

    public double getY_koordinata_a() {
        return y_koordinata_a;
    }

    public void setY_koordinata_a(double y_koordinata_a) {
        this.y_koordinata_a = y_koordinata_a;
    }

    public double getX_koordinata_b() {
        return x_koordinata_b;
    }

    public void setX_koordinata_b(double x_koordinata_b) {
        this.x_koordinata_b = x_koordinata_b;
    }

    public double getY_koordinata_b() {
        return y_koordinata_b;
    }

    public void setY_koordinata_b(double y_koordinata_b) {
        this.y_koordinata_b = y_koordinata_b;
    }

    public double getKm_izmedu_a_b() {
        return km_izmedu_a_b;
    }

    public void setKm_izmedu_a_b(double km_izmedu_a_b) {
        this.km_izmedu_a_b = km_izmedu_a_b;
    }

    public double getProsjecna_brzina() {
        return prosjecna_brzina;
    }

    public void setProsjecna_brzina(double prosjecna_brzina) {
        this.prosjecna_brzina = prosjecna_brzina;
    }
    
}
