@Back-End
Feature: Write To And Read From A Database
    AS A BACK-END TEST AUTOMATION ENGINEER
    I SHOULD BE ABLE TO WRITE TO AND READ FROM A DATABASE


    Background:
        Given   THE DATABASE CONNECTION IS OPEN


    Scenario: [01/01] Write And Read Data To A Database
        Given   I DROP ANY ALREADY-EXISTING "ENTRIES" TABLE
        When    I CREATE A NEW "ENTRIES" TABLE
        And     I INSERT DATA INTO THE "ENTRIES" TABLE

                    | LANGUAGE     | TRANSLATION           |
                    | Arabic       | Marhaban, Bialealami! |
                    | Chinese      | Nǐ Hǎo, Shìjiè!       |
                    | English      | Hello, World!         |
                    | Esperanto    | Saluton, Mondo!       |
                    | Filipino     | Hello, Mundo!         |
                    | French       | Bonjour, Monde!       |
                    | German       | Hallo, Welt!          |
                    | Greek        | Geiá Sou, Kósme!      |
                    | Hindi        | Namaste, Duniya!      |
                    | Italian      | Ciao, Mondo!          |
                    | Japanese     | Kon`nichiwa, Sekai!   |
                    | Romanian     | Salut, Lume!          |
                    | Scots Gaelic | Hàlo, A Shaoghail!    |
                    | Spanish      | ¡Hola, Mundo!         |
                    | Yiddish      | Vela, Velt!           |

        Then    I CONFIRM THAT THE "ENTRIES" TABLE HAS "15" ROWS
