/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package datahandlers;

import db.dbHandler;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Paths;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.w3c.dom.Document;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import models.Ruta;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 *
 * @author keljo
 */
public class xmlDataHandler {
    private static dbHandler db = dbHandler.getInstance();

    public void fullDatabaseRestore(String dbname) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void fullDatabaseBackup(String dbname) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int exportRute(int putni_nalog_id,String path, String fname) {
        List<Ruta> l = db.SelectRute(putni_nalog_id);
        int n = 0;
        try {
            FileWriter fWriter = new FileWriter(Paths.get(path,fname).toFile());
            fWriter.append(String.format(
                    "<?xml version=\"1.0\"?>\n"
                    + "<putni_nalog id=\"%d\">\n"
                    ,putni_nalog_id));
            
            for(Ruta r : l){
                fWriter.append(
                    String.format(
                        "    <ruta id=\"%d\">\n"
                        + "        <x_koordinata_a>%f</x_koordinata_a>\n"
                        + "        <y_koordinata_a>%f</y_koordinata_a>\n"
                        + "        <x_koordinata_b>%f</x_koordinata_b>\n"
                        + "        <y_koordinata_b>%f</y_koordinata_b>\n"
                        + "        <km_izmedu_a_b>%f</km_izmedu_a_b>\n"
                        + "        <prosjecna_brzina>%f</prosjecna_brzina>\n"
                        + "    </ruta>\n",
                        r.getId(),r.getX_koordinata_a(),
                        r.getY_koordinata_a(),r.getX_koordinata_b(),
                        r.getY_koordinata_b(),r.getKm_izmedu_a_b(),r.getProsjecna_brzina()
                    )
                );
                ++n;
            }
            fWriter.append("</putni_nalog>\n");
            fWriter.close();
        } catch (IOException ex) {
            Logger.getLogger(xmlDataHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
        return n;
    }

    public int importRute(String path) {
        try{
            File fXmlFile = new File(path);
            DocumentBuilder dBuilder = DocumentBuilderFactory.newInstance()
                             .newDocumentBuilder();

	    Document doc = dBuilder.parse(fXmlFile);
            doc.getDocumentElement().normalize();
            NodeList nList = doc.getElementsByTagName("ruta");
            int temp = 0;
            for(; temp<nList.getLength(); ++temp){
                Node nNode = nList.item(temp);
                if (nNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element eElement = (Element) nNode;
                    int putni_nalog_id =      Integer.parseInt(eElement.getElementsByTagName("putni_nalog_id").item(0).getTextContent());
                    double x_koordinata_a =   Double.parseDouble(eElement.getElementsByTagName("x_koordinata_a").item(0).getTextContent());
                    double y_koordinata_a =   Double.parseDouble(eElement.getElementsByTagName("y_koordinata_a").item(0).getTextContent());
                    double x_koordinata_b =   Double.parseDouble(eElement.getElementsByTagName("x_koordinata_b").item(0).getTextContent());
                    double y_koordinata_b =   Double.parseDouble(eElement.getElementsByTagName("y_koordinata_b").item(0).getTextContent());
                    double km_izmedu_a_b =    Double.parseDouble(eElement.getElementsByTagName("km_izmedu_a_b").item(0).getTextContent());
                    double prosjecna_brzina = Double.parseDouble(eElement.getElementsByTagName("prosjecna_brzina").item(0).getTextContent());
                    Ruta r = new Ruta(
                            putni_nalog_id,
                            x_koordinata_a,y_koordinata_a,
                            x_koordinata_b,y_koordinata_b,
                            km_izmedu_a_b,
                            prosjecna_brzina
                    );
                    db.InsertRuta(r);
		}
            }
            return temp;
        }catch (Exception e){
            e.printStackTrace();
        }
        return 0;
    }
    
}
