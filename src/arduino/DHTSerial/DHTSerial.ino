#include <DHT.h>
#include<ArduinoJson.h>
#define DHTPIN 2
#define DHTTYPE DHT11
#define maxMediciones 3

const int espera = 2000;
const size_t bufferSize = JSON_ARRAY_SIZE(maxMediciones) + maxMediciones*JSON_OBJECT_SIZE(2);
const char cadHumedad[]="Humedad";
const char cadTemperatura[]="Temperatura";
const char cadSensacionTermica[]="Sensacion Termica";
DynamicJsonBuffer jsonBuffer(bufferSize);
DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
  DynamicJsonBuffer jsonBuffer(bufferSize);
}

void loop() {
  delay(espera);

  float h = dht.readHumidity();
  float t = dht.readTemperature();
  if (verificarMediciones(h,t)) {
    Serial.println("Error obteniendo los datos del sensor DHT11");
    return;
  }
  // Calcular el índice de calor en grados centígrados
  float hic = dht.computeHeatIndex(t, h, false);

  //String cadena = armarCadena(h, t, hic);
  //Serial.println(cadena);
  enviarJsonPorSerial(h,t,hic);
  Serial.println();
}

String armarCadena(float humedad, float temperatura, float indiceCalor){
  //Armo parte de humedad;
  String cadena = "Humedad: ";
  cadena.concat(humedad);
  cadena.concat(" %\tTemperatura: ");
  
  //Armo la temperatura
  cadena.concat(temperatura);
  cadena.concat(" *C\tÍndice de calor: ");

  //Armo el indice de calor
  cadena.concat(indiceCalor);
  cadena.concat(" *C");

  return cadena;
}

bool verificarMediciones(float humedad, float temperatura){
  return isnan(humedad) || isnan(temperatura);
}

void enviarJsonPorSerial(float humedad, float temperatura, float indiceCalor){
  JsonArray& mediciones = jsonBuffer.createArray();

  JsonObject& mediciones_0 = mediciones.createNestedObject();
  mediciones_0["n"] = cadHumedad;
  mediciones_0["v"] = humedad;

  JsonObject& mediciones_1 = mediciones.createNestedObject();
  mediciones_1["n"] = cadTemperatura;
  mediciones_1["v"] = temperatura;

  JsonObject& mediciones_2 = mediciones.createNestedObject();
  mediciones_2["n"] = cadSensacionTermica;
  mediciones_2["v"] = indiceCalor;

  //mediciones.prettyPrintTo(Serial);
  mediciones.printTo(Serial);
  jsonBuffer.clear();
}
