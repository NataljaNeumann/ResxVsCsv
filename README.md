
‎
# Resx Vs. Csv
  
[Latest Sources and Releases](https://github.com/NataljaNeumann/ResxVsCsv)  
  
  
![ResxVsCsv2](https://github.com/user-attachments/assets/03b7bdce-d227-4eb3-a507-f9941f51f6d4)  

‎[English](#en), [Français](#fr), [Español](#es), [Português](#pt), [Italiano](#it), [Deutsch](#de), [По русски](#ru), [Polski](#pl), [Στα ελληνικά](#gr), 
      [Nederlands](#nl), [Dansk](#da), [Suomeksi](#fi), [Svenska](#sv), [Türkçe](#tr), [中文文本](#chs), [中文文字](#cht), [日本語](#ja), [한국인](#ko), [भारतीय में](#hi), [باللغة العربية](#ar), [עִברִית](#he)
‎
# English
<a name="en"></a>
‎This project transforms RESX files to CSV and back. Also some functionality is added for automatic translations of strings.

‎Parameters:

‎‎--directory &lt;dir&gt;‎ the directory of RESX files or of CSV file, e.g.: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ the patttern for RESX files, e.g.: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ the translator for translation of missing RESX entries during transfer to CSV, e.g.: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ the key for translation of api, e.g.: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ the URL for libretranslate translation service, e.g.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ specifies that CSV file shall be sorted by name, yes or no. Default is: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ the specified CSV file shall be integrated into corresponding RESX files, e.g.: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ specifies, if only string values shall be gathered in CSV, or other values, too. Yes or no, the default is yes, e.g.: ‎--onlystrings no‎

‎For conversion to CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎For translation: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎For translation with Argos: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎For translation with LibreTranslate: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎For updating RESX files: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Do you need support?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Français
<a name="fr"></a>
‎Ce projet transforme les fichiers RESX en CSV et inversement. Certaines fonctionnalités sont également ajoutées pour les traductions automatiques de chaînes.

‎Paramètres:

‎‎--directory &lt;dir&gt;‎ le répertoire des fichiers RESX ou du fichier CSV, par exemple: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ le modèle pour les fichiers RESX, par exemple: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ le traducteur pour la traduction des entrées RESX manquantes lors du transfert vers CSV, par exemple: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ la clé de traduction de l'API, par exemple: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ l'URL du service de traduction libretranslate, par exemple: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ spécifie que le fichier CSV doit être trié par nom, oui ou non. La valeur par défaut est: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ le fichier CSV spécifié doit être intégré aux fichiers RESX correspondants, par exemple: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ spécifie si seules les valeurs de chaîne doivent être collectées au format CSV, ou d'autres valeurs également. Oui ou non, la valeur par défaut est oui, par exemple: ‎--onlystrings no‎

‎Pour la conversion en CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Pour la traduction: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Pour la traduction avec «Argos»: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Pour la traduction avec «LibreTranslate»: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Pour mettre à jour les fichiers RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Avez-vous besoin de soutien?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wikia](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Español
<a name="es"></a>
‎Este proyecto transforma archivos RESX a CSV y viceversa. También se agrega alguna funcionalidad para traducciones automáticas de cadenas.

‎Parámetros:

‎‎--directory &lt;dir&gt;‎ el directorio de archivos RESX o de archivos CSV, por ejemplo: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ el patrón para archivos RESX, por ejemplo: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ tel traductor para traducir las entradas RESX que faltan durante la transferencia a CSV, por ejemplo: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ la clave para la traducción de api, por ejemplo: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ la URL del servicio de traducción libretranslate, por ejemplo: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ especifica que el archivo CSV se ordenará por nombre, sí o no. El valor predeterminado es: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ El archivo CSV especificado se integrará en los archivos RESX correspondientes, por ejemplo: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ especifica si solo se recopilarán valores de cadena en CSV, u otros valores también. Sí o no, el valor predeterminado es sí, por ejemplo: ‎--onlystrings no‎

‎Para conversión a CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Para traducción: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Para traducir con "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Para traducir con "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Para actualizar archivos RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[¿Necesitas apoyo?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Português
<a name="pt"></a>
‎Este projeto transforma arquivos RESX em CSV e vice-versa. Também foram adicionadas algumas funcionalidades para traduções automáticas de strings.

‎Parâmetros:

‎‎--directory &lt;dir&gt;‎ o diretório de arquivos RESX ou de arquivo CSV, por exemplo: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ o padrão para arquivos RESX, por exemplo: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ o tradutor para tradução de entradas RESX ausentes durante a transferência para CSV, por exemplo: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ a chave para tradução da API, por exemplo: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ o URL do serviço de tradução libretranslate, por exemplo: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ especifica que o arquivo CSV deve ser classificado por nome, sim ou não. O padrão é: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ o arquivo CSV especificado deve ser integrado aos arquivos RESX correspondentes, por exemplo: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ especifica se apenas valores de string devem ser reunidos em CSV ou outros valores também. Sim ou não, o padrão é sim, por exemplo: ‎--onlystrings no‎

‎Para conversão para CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Para tradução: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Para tradução com "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎FPara tradução com "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Para atualizar arquivos RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Você precisa de suporte?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Italiano
<a name="it"></a>
‎Questo progetto trasforma i file RESX in CSV e viceversa. Inoltre vengono aggiunte alcune funzionalità per la traduzione automatica delle stringhe.

‎Parametri:

‎‎--directory &lt;dir&gt;‎ la directory dei file RESX o del file CSV, ad esempio: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ il modello per i file RESX, ad esempio: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ il traduttore per la traduzione delle voci RESX mancanti durante il trasferimento in CSV, ad esempio: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ la chiave per la traduzione di api, ad esempio: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ l'URL del servizio di traduzione libretranslate, ad esempio: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ specifica che il file CSV deve essere ordinato per nome, sì o no. L'impostazione predefinita è: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ il file CSV specificato deve essere integrato nei file RESX corrispondenti, ad esempio: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ specifica se nel CSV devono essere raccolti solo valori stringa o anche altri valori. Sì o no, l'impostazione predefinita è sì, ad esempio: ‎--onlystrings no‎

‎Per la conversione in CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Per la traduzione: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Per la traduzione con "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Per la traduzione con "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Per aggiornare i file RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Hai bisogno di supporto?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Deutsch
<a name="de"></a>
‎Dieses Projekt wandelt RESX-Dateien in CSV und zurück um. Außerdem wurden einige Funktionen für die automatische Übersetzung von Zeichenfolgen hinzugefügt.

‎Parameter:

‎‎--directory &lt;dir&gt;‎ das Verzeichnis der RESX-Dateien oder der CSV-Datei, z.B.: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ das Muster für RESX-Dateien, z.B.: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ der Übersetzer zur Übersetzung fehlender RESX-Einträge bei der Übertragung nach CSV, z.B.: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ Der Schlüssel für die Übersetzung der API, z.B.: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ die URL für den Übersetzungsdienst libretranslate, z. B.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ Gibt an, dass die CSV-Datei nach Name sortiert werden soll, ja oder nein. Standard ist: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ Die angegebene CSV-Datei muss in entsprechende RESX-Dateien integriert werden, z. B.: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ Gibt an, ob nur String-Werte in CSV erfasst werden sollen, oder auch andere Werte. Ja oder nein, die Standardeinstellung ist ja, z. B.: ‎--onlystrings no‎

‎Für die Konvertierung in CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Zur Übersetzung: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Zur Übersetzung mit „Argos“: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Zur Übersetzung mit „LibreTranslate“: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Zum Aktualisieren von RESX-Dateien: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Brauchen Sie Unterstützung?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# По русски
<a name="ru"></a>
‎Этот проект преобразует файлы RESX в CSV и обратно. Также добавлен функционал для автоматического перевода строк.

‎Параметры:

‎‎--directory &lt;dir&gt;‎ каталог файлов RESX или файла CSV, например: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ шаблон для файлов RESX, например: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ переводчик для перевода недостающих записей RESX при переносе в CSV, например: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ ключ для перевода API, например: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ URL-адрес службы перевода libretranslate, например: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ указывает, что файл CSV должен быть отсортирован по имени, да или нет. По умолчанию: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ указанный файл CSV должен быть интегрирован в соответствующие файлы RESX, например: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ указывает, следует ли собирать в CSV только строковые значения или другие значения. Да или нет, по умолчанию — да, например: ‎--onlystrings no‎

‎Для преобразования в CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Для перевода: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Для перевода с помощью «Argos»: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Для перевода с помощью «LibreTranslate»: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Для обновления файлов RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Вам нужна поддержка?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Вики](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Polski
<a name="pl"></a>
‎Ten projekt przekształca pliki RESX do formatu CSV i odwrotnie. Dodano także pewną funkcjonalność automatycznego tłumaczenia ciągów znaków.

‎Parametry:

‎‎--directory &lt;dir&gt;‎ katalog plików RESX lub pliku CSV, np.: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ wzór dla plików RESX, np.: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ tłumacz do tłumaczenia brakujących wpisów RESX podczas przesyłania do CSV, np.: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ klucz do tłumaczenia API, np.: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ adres URL usługi tłumaczeniowej libretranslate, np.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ określa, że ​​plik CSV będzie sortowany według nazwy, tak lub nie. Wartość domyślna to: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ określony plik CSV należy zintegrować z odpowiednimi plikami RESX, np.: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ określa, czy w formacie CSV mają być gromadzone tylko wartości łańcuchowe, czy też inne wartości. Tak lub nie, domyślnie jest to tak, np.: ‎--onlystrings no‎

‎Do konwersji do CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Do tłumaczenia: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Do tłumaczenia z „Argos”: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Do tłumaczenia za pomocą „LibreTranslate”: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Aby zaktualizować pliki RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Czy potrzebujesz wsparcia?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Вики](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Στα ελληνικά
<a name="gr"></a>
‎Αυτό το έργο μετατρέπει αρχεία RESX σε CSV και πίσω. Επίσης, προστίθεται κάποια λειτουργικότητα για αυτόματες μεταφράσεις συμβολοσειρών.

‎Παράμετροι:

‎‎--directory &lt;dir&gt;‎ τον κατάλογο των αρχείων RESX ή του αρχείου CSV, π.χ. ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ το μοτίβο για αρχεία RESX, π.χ. ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ ο μεταφραστής για μετάφραση των καταχωρήσεων RESX που λείπουν κατά τη μεταφορά σε CSV, π.χ. ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ το κλειδί για τη μετάφραση του api, π.χ. ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ τη διεύθυνση URL για την υπηρεσία μετάφρασης libretranslate, π.χ.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ καθορίζει ότι το αρχείο CSV θα ταξινομηθεί με βάση το όνομα, ναι ή όχι. Η προεπιλογή είναι: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ το καθορισμένο αρχείο CSV θα ενσωματωθεί σε αντίστοιχα αρχεία RESX, π.χ. ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ καθορίζει, εάν θα συγκεντρώνονται μόνο τιμές συμβολοσειράς στο CSV ή και άλλες τιμές. Ναι ή όχι, η προεπιλογή είναι ναι, π.χ. ‎--onlystrings no‎

‎Για μετατροπή σε CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Για μετάφραση: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Για μετάφραση με το "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Για μετάφραση με το "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Για ενημέρωση αρχείων RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Χρειάζεστε υποστήριξη;](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Nederlands
<a name="nl"></a>
‎Dit project transformeert RESX-bestanden naar CSV en terug. Ook is er functionaliteit toegevoegd voor automatische vertalingen van strings.

‎Parameters:

‎‎--directory &lt;dir&gt;‎ de map met RESX-bestanden of een CSV-bestand, bijvoorbeeld: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ het patroon voor RESX-bestanden, bijvoorbeeld: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ de vertaler voor de vertaling van ontbrekende RESX-gegevens tijdens de overdracht naar CSV, bijvoorbeeld: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ de sleutel voor de vertaling van API, bijvoorbeeld: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ de URL voor de libretranslate-vertaalservice, bijvoorbeeld: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ specificeert dat het CSV-bestand moet worden gesorteerd op naam, ja of nee. Standaard is: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ het gespecificeerde CSV-bestand wordt geïntegreerd in overeenkomstige RESX-bestanden, bijvoorbeeld: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ specificeert of alleen tekenreekswaarden moeten worden verzameld in CSV, of ook andere waarden. Ja of nee, de standaardwaarde is ja, bijvoorbeeld: ‎--onlystrings no‎

‎Voor conversie naar CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Voor vertaling: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Voor vertaling met "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Voor vertaling met "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Voor het bijwerken van RESX-bestanden: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Heeft u ondersteuning nodig?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Dansk
<a name="da"></a>
‎Dette projekt transformerer RESX-filer til CSV og tilbage. Der er også tilføjet nogle funktioner til automatiske oversættelser af strenge.

‎Parametre:

‎‎--directory &lt;dir&gt;‎ biblioteket med RESX-filer eller CSV-filer, f.eks.: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ mønsteret for RESX-filer, f.eks.: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ oversætteren til oversættelse af manglende RESX-poster under overførsel til CSV, f.eks.: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ nøglen til oversættelse af api, f.eks.: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ URL'en til libretranslate-oversættelsestjenesten, f.eks.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ specificerer, at CSV-filen skal sorteres efter navn, ja eller nej. Standard er: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ den angivne CSV-fil skal integreres i tilsvarende RESX-filer, f.eks.: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ angiver, om kun strengværdier skal samles i CSV, eller også andre værdier. Ja eller nej, standarden er ja, f.eks.: ‎--onlystrings no‎

‎For konvertering til CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Til oversættelse: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Til oversættelse med "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Til oversættelse med "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎Til opdatering af RESX-filer: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Har du brug for støtte?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Suomeksi
<a name="fi"></a>
‎Tämä projekti muuttaa RESX-tiedostot CSV-muotoon ja takaisin. Myös joitakin toimintoja on lisätty merkkijonojen automaattista kääntämistä varten.

‎Parametrit:

‎‎--directory &lt;dir&gt;‎ RESX-tiedostojen tai CSV-tiedostojen hakemisto, esim. ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX-tiedostojen malli, esim. ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ kääntäjä puuttuvien RESX-merkintöjen kääntämiseen CSV-tiedostoon siirron aikana, esim. ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ avain api:n kääntämiseen, esim. ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate-käännöspalvelun URL-osoite, esim.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ määrittää, että CSV-tiedosto lajitellaan nimen mukaan, kyllä ​​tai ei. Oletus on: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ määritetty CSV-tiedosto tulee integroida vastaaviin RESX-tiedostoihin, esim. ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ määrittää, kerätäänkö CSV:hen vain merkkijonoarvot vai myös muita arvoja. Kyllä tai ei, oletusarvo on kyllä, esim.: ‎--onlystrings no‎

‎CSV-muotoon muuntaminen: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Käännös: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎Käännös sanalla "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎Käännös sanalla "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎RESX-tiedostojen päivittäminen: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Tarvitsetko tukea?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Svenska
<a name="sv"></a>
‎Detta projekt omvandlar RESX-filer till CSV och tillbaka. Även viss funktionalitet läggs till för automatisk översättning av strängar.

‎Parametrar:

‎‎--directory &lt;dir&gt;‎ katalogen för RESX-filer eller CSV-filer, t.ex.: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ mönstret för RESX-filer, t.ex.: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ översättaren för översättning av saknade RESX-poster under överföring till CSV, t.ex.: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ nyckeln för översättning av api, t.ex.: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ URL:en för översättningstjänsten libretranslate, t.ex.: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ anger att CSV-filen ska sorteras efter namn, ja eller nej. Standard är: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ den angivna CSV-filen ska integreras i motsvarande RESX-filer, t.ex.: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ anger om endast strängvärden ska samlas in i CSV, eller andra värden också. Ja eller nej, standard är ja, t.ex.: ‎--onlystrings no‎

‎För konvertering till CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎För översättning: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎För översättning med "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎För översättning med "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎För att uppdatera RESX-filer: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Behöver du stöd?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Wiki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# Türkçe
<a name="tr"></a>
‎Bu proje RESX dosyalarını CSV'ye ve geriye dönüştürür. Ayrıca dizelerin otomatik çevirileri için bazı işlevler eklenmiştir.

‎Parametreler:

‎‎--directory &lt;dir&gt;‎ RESX dosyalarının veya CSV dosyasının dizini, örneğin: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX dosyaları için kalıp, örneğin: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ CSV'ye aktarım sırasında eksik RESX girişlerinin çevirisi için çevirmen, örneğin: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ api'nin çevirisinin anahtarı, örneğin: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate çeviri hizmetinin URL'si, örneğin: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ CSV dosyasının evet veya hayır ada göre sıralanacağını belirtir. Varsayılan: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ belirtilen CSV dosyası ilgili RESX dosyalarına entegre edilecektir, örneğin: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ CSV'de yalnızca dize değerlerinin mi yoksa diğer değerlerin mi toplanacağını belirtir. Evet veya hayır, varsayılan evet'tir, örneğin: ‎--onlystrings no‎

‎CSV'ye dönüştürmek için: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎Çeviri için: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎"Argos" ile çeviri için: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎"LibreTranslate" ile çeviri için: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎RESX dosyalarını güncellemek için: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[Desteğe mi ihtiyacınız var?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[Viki](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# 中文文本
<a name="chs"></a>
‎该项目将 RESX 文件与 CSV 文件相互转换。还添加了一些用于字符串自动翻译的功能。

‎参数：

‎‎--directory &lt;dir&gt;‎ RESX 文件或 CSV 文件的目录，例如： ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX 文件的模式，例如： ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ 翻译器用于在传输到 CSV 期间翻译丢失的 RESX 条目，例如： ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ API 翻译的关键，例如： ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate 翻译服务的 URL，例如： ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ 指定 CSV 文件应按名称、是或否排序。默认为： ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ 指定的 CSV 文件应集成到相应的 RESX 文件中，例如： ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ 指定是否仅应在 CSV 中收集字符串值，或者也应收集其他值。是或否，默认为是，例如： ‎--onlystrings no‎

‎对于转换为 CSV： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎翻译： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎对于「Argos」翻译： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎使用「LibreTranslate」进行翻译： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎对于更新 RESX 文件： ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[您需要支持吗？](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[维基百科](https://github.com/NataljaNeumann/ResxVsCsv/wiki)  
‎
# 中文文字
<a name="cht"></a>
‎該專案將 RESX 檔案與 CSV 檔案相互轉換。還添加了一些用於字串自動翻譯的功能。

‎參數：

‎‎--directory &lt;dir&gt;‎ RESX 檔案或 CSV 檔案的目錄，例如： ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX 檔案的模式，例如： ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ 在傳輸到 CSV 期間翻譯遺失的 RESX 條目的翻譯器，例如： ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ API 翻譯的金鑰，例如： ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate 翻譯服務的 URL，例如： ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ 指定 CSV 檔案應按名稱（是或否）排序。預設為： ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ 指定的 CSV 檔案應整合到對應的 RESX 檔案中，例如： ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ 指定是否僅將字串值收集在 CSV 中，或也收集其他值。是或否，預設為是，例如： ‎--onlystrings no‎

‎對於轉換為 CSV： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎對於翻譯： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎對於使用「Argos」進行翻譯：‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎對於使用「LibreTranslate」進行翻譯： ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎對於更新 RESX 檔案： ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[您需要支援嗎？](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[維基百科](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# 日本語
<a name="ja"></a>
‎このプロジェクトは、RESX ファイルを CSV に変換したり、その逆の変換を行ったりします。また、文字列の自動翻訳のための機能もいくつか追加されています。

‎パラメータ:

‎‎--directory &lt;dir&gt;‎ RESX ファイルまたは CSV ファイルのディレクトリ。例: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX ファイルのパターン、例: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ CSV への転送中に欠落している RESX エントリを翻訳するためのトランスレータ。例: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ API の翻訳用のキー、例: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate 翻訳サービスの URL、例: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ CSV ファイルを名前でソートすることを指定します (「はい」または「いいえ」)。デフォルトは: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ 指定された CSV ファイルは、対応する RESX ファイルに統合されます。例: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ 文字列値のみを CSV で収集するか、他の値も収集するかどうかを指定します。 「はい」または「いいえ」。デフォルトは「はい」です。例: ‎--onlystrings no‎

‎CSV に変換するには: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎翻訳の場合: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎「Argos」を使用した翻訳の場合: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎「LibreTranslate」による翻訳の場合: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎RESX ファイルを更新するには: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[サポートが必要ですか?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[ウィキ](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# 한국인
<a name="ko"></a>
‎이 프로젝트는 RESX 파일을 CSV로 변환하거나 그 반대로 변환합니다. 또한 문자열 자동 번역을 위한 일부 기능이 추가되었습니다.

‎매개변수:

‎‎--directory &lt;dir&gt;‎ RESX 파일 또는 CSV 파일의 디렉터리입니다. 예: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX 파일의 패턴, 예: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ CSV로 전송하는 동안 누락된 RESX 항목을 번역하기 위한 번역기, 예: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ API 번역의 키입니다. 예: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ libretranslate 번역 서비스의 URL, 예: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ CSV 파일이 이름(예 또는 아니요)별로 정렬되도록 지정합니다. 기본값은: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ 지정된 CSV 파일은 해당 RESX 파일에 통합되어야 합니다. 예: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ 문자열 값만 CSV로 수집할지, 아니면 다른 값도 수집할지를 지정합니다. 예 또는 아니요. 기본값은 예입니다. 예: ‎--onlystrings no‎

‎CSV로 변환하려면: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎번역: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎"Argos"로 번역하려면: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎"LibreTranslate"를 사용한 번역: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎RESX 파일을 업데이트하려면: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[지원이 필요합니까?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[위키](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‎
# भारतीय में
<a name="hi"></a>
‎यह प्रोजेक्ट RESX फ़ाइलों को CSV और बैक में बदल देता है। इसके अलावा स्ट्रिंग्स के स्वचालित अनुवाद के लिए कुछ कार्यक्षमता भी जोड़ी गई है।

‎पैरामीटर:

‎‎--directory &lt;dir&gt;‎ RESX फ़ाइलों या CSV फ़ाइल की निर्देशिका, जैसे: ‎--directory c:\users\myname\projects\myproject‎

‎‎--pattern &lt;pattern&gt;‎ RESX फ़ाइलों के लिए पैटर्न, उदाहरण: ‎--pattern Resources.*resx‎

‎‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‎ CSV में स्थानांतरण के दौरान गुम RESX प्रविष्टियों के अनुवाद के लिए अनुवादक, उदाहरण: ‎--translator argos‎

‎‎--apikey &lt;key&gt;‎ एपीआई के अनुवाद के लिए कुंजी, उदाहरण: ‎--key 012345XYZ‎

‎‎--libreurl &lt;url&gt;‎ लिब्रेट्रांसलेट अनुवाद सेवा के लिए यूआरएल, उदाहरण: ‎--libreurl https://libretranslate.com/translate‎

‎‎--sortbyname &lt;yes|no&gt;‎ निर्दिष्ट करता है कि CSV फ़ाइल को नाम, हाँ या नहीं के आधार पर क्रमबद्ध किया जाएगा। डिफ़ॉल्ट है: ‎no‎

‎‎--toresx &lt;name.csv&gt;‎ निर्दिष्ट CSV फ़ाइल को संबंधित RESX फ़ाइलों में एकीकृत किया जाएगा, उदाहरण के लिए: ‎--toresx Resources.csv‎

‎‎--onlystrings &lt;yes|no&gt;‎ निर्दिष्ट करता है, यदि सीएसवी में केवल स्ट्रिंग मान एकत्र किए जाएंगे, या अन्य मान भी। हाँ या नहीं, डिफ़ॉल्ट हाँ है, उदाहरण: ‎--onlystrings no‎

‎सीएसवी में रूपांतरण के लिए: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‎

‎अनुवाद के लिए: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‎

‎"Argos" के साथ अनुवाद के लिए: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‎

‎"LibreTranslate" के साथ अनुवाद के लिए: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‎

‎RESX फ़ाइलें अद्यतन करने के लिए: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‎
  
[क्या आपको समर्थन की आवश्यकता है?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[विकि](https://github.com/NataljaNeumann/ResxVsCsv/wiki)‏
# باللغة العربية
<a name="ar"></a>
‏يقوم هذا المشروع بتحويل ملفات RESX إلى CSV والعكس. تتم أيضًا إضافة بعض الوظائف للترجمات التلقائية للسلاسل.

‏حدود:

‏‎--directory &lt;dir&gt;‏ دليل ملفات RESX أو ملف CSV، على سبيل المثال: ‎--directory c:\users\myname\projects\myproject‏

‏‎--pattern &lt;pattern&gt;‏ النمط لملفات RESX، على سبيل المثال: ‎--pattern Resources.*resx‏

‏‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‏ مترجم لترجمة إدخالات RESX المفقودة أثناء النقل إلى ملف CSV، على سبيل المثال: ‎--translator argos‏

‏‎--apikey &lt;key&gt;‏ مفتاح ترجمة واجهة برمجة التطبيقات، على سبيل المثال: ‎--key 012345XYZ‏

‏‎--libreurl &lt;url&gt;‏ عنوان URL لخدمة الترجمة libretranslate، على سبيل المثال: ‎--libreurl https://libretranslate.com/translate‏

‏‎--sortbyname &lt;yes|no&gt;‏ يحدد أنه يجب فرز ملف CSV حسب الاسم، نعم أو لا. الافتراضي هو: ‎no‏

‏‎--toresx &lt;name.csv&gt;‏ يجب دمج ملف CSV المحدد في ملفات RESX المقابلة، على سبيل المثال: ‎--toresx Resources.csv‏

‏‎--onlystrings &lt;yes|no&gt;‏ يحدد ما إذا كان سيتم جمع قيم السلسلة فقط في ملف CSV، أو قيم أخرى أيضًا. نعم أو لا، الافتراضي هو نعم، على سبيل المثال: ‎--onlystrings no‏

‏للتحويل إلى CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‏

‏للترجمة: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‏

‏للترجمة مع "أرجوس": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‏

‏للترجمة باستخدام "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‏

‏لتحديث ملفات RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‏

‏[هل تحتاج إلى دعم؟](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[ويكي](https://github.com/NataljaNeumann/ResxVsCsv/wiki)
‏
# עִברִית
<a name="he"></a>
‏פרויקט זה הופך קבצי RESX ל-CSV ובחזרה. כמו כן, מתווספת פונקציונליות מסוימת עבור תרגומים אוטומטיים של מחרוזות.

‏פרמטרים:

‏‎--directory &lt;dir&gt;‏ הספרייה של קבצי RESX או של קובץ CSV, למשל: ‎--directory c:\users\myname\projects\myproject‏

‏‎--pattern &lt;pattern&gt;‏ התבנית של קבצי RESX, למשל: ‎--pattern Resources.*resx‏

‏‎--translator &lt;google|microsoft|deepl|toptranslation|libretranslate|argos&gt;‏ המתרגם לתרגום של ערכי RESX חסרים במהלך העברה ל-CSV, למשל: ‎--translator argos‏

‏‎--apikey &lt;key&gt;‏ המפתח לתרגום API, למשל: ‎--key 012345XYZ‏

‏‎--libreurl &lt;url&gt;‏ כתובת האתר של שירות התרגום libretranslate, למשל: ‎--libreurl https://libretranslate.com/translate‏

‏‎--sortbyname &lt;yes|no&gt;‏ מציין שקובץ ה-CSV ימויין לפי שם, כן או לא. ברירת המחדל היא: ‎no‏

‏‎--toresx &lt;name.csv&gt;‏ קובץ ה-CSV שצוין ישולבו בקבצי RESX מתאימים, למשל: ‎--toresx Resources.csv‏

‏‎--onlystrings &lt;yes|no&gt;‏ מציין, אם רק ערכי מחרוזת יאספו ב-CSV, או גם בערכים אחרים. כן או לא, ברירת המחדל היא כן, למשל: ‎--onlystrings no‏

‏להמרה ל-CSV: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; [--sortbyname yes] [--onlystrings no]‏

‏לתרגום: ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator &lt;google|microsoft|deepl|toptranslation&gt; --apikey &lt;key&gt; [--sortbyname yes]‏

‏לתרגום עם "Argos": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt;  --translator argos [--sortbyname yes]‏

‏לתרגום עם "LibreTranslate": ‎ResxVsCsv --directory &lt;dir&gt; --pattern &lt;pattern&gt; --translator libretranslate --libreurl &lt;url&gt; [--apikey &lt;key&gt;] [--sortbyname yes]‏

‏לעדכון קבצי RESX: ‎ResxVsCsv --directory &lt;dir&gt; --toresx &lt;resources.csv&gt;‏

‏[האם אתה צריך תמיכה?](https://github.com/NataljaNeumann/ResxVsCsv/issues)  
[ויקי](https://github.com/NataljaNeumann/ResxVsCsv/wiki)

