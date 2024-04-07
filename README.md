# AfrikAI
*Készítették:
Podlipnik Ádám  
Tédi Bálint*

Az **AfrikAI** egy játékos implementációja több útkereső algoritmusnak. A játékosnak egy zebrának kell segítenie vizet találni egy random generált, vagy akár a játékos által létrehozott sivatagban.

#### A sivatag felépítése 
> A téglalap alakú sivatagon megtalálható mezők típusa a következő lehet, különböző szerepekkel:
> 1. homok
> 1. fal
> 1. oroszlán
> 1. víz
> 1. zebra
#### A játék célja 
> A zebrának a falakat és az oroszlánokat kikerülve kell eljutnia a vízhez. Mindeközben, az oroszlánok folyamatosan közelednek felé, szintén egy útkereső algoritmus segítségével.
> A játékos létrehozhat saját sivatagot, generáltathat egyet véletlenszerűen, majd segíthet a zebrának vizet találni.
#### A játék menete
> A játékos minden körben felcserélhet kétszer két mezőt, amennyiben azok nem állatot vagy vizet tartalmaznak.
> Az állatok minden körben egy mezőnyit lépnek, a zebra a víz, az oroszlán pedig a zebra felé.
> Ha a zebra elérte a vizet, a játékos győzött, ám ha az oroszlánok egyike utolérte a zebrát, vége a játéknak.
#### A szerkesztő működése
> A szerkesztő megnyitása után a felhasználó egy üres sivatagot talál a képernyőjén, amelyen a nyíl billentyűkkel navigálva a [+] és [-] billentyűk segítségével tudja egy mező típusát megváltoztatni.
> Mindezek után a [V] gomb segítségével elmenthető a létrehozott pálya.
