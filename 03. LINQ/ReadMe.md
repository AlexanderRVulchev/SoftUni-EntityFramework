People love listening to music, but they see that YouTube is getting older and older. You want to make people happy and you've decided to make a better version of YouTube – MusicHub. It's time for you to start coding. Good luck and impress us.</br>

### 1.	MusicHub Database
You must create a database for a MusicHub. It should look like this:</br>
 
 ![image](https://user-images.githubusercontent.com/106471266/221131435-648281b0-f626-4929-8074-9c86875ab71d.png)

Constraints</br>
Your namespaces should be:</br>
•	MusicHub – for your StartUp class, if you have one</br>
•	MusicHub.Data – for your DbContext</br>
•	MusicHub.Data.Models – for your Models</br>

Your models should be:</br>
Song</br>
•	Id – integer, Primary Key</br>
•	Name – text with max length 20 (required)</br>
•	Duration – TimeSpan (required)</br>
•	CreatedOn – date (required)</br>
•	Genre ¬– genre enumeration with possible values: "Blues, Rap, PopMusic, Rock, Jazz" (required)</br>
•	AlbumId – integer, Foreign key</br>
•	Album – the Song's Album</br>
•	WriterId – integer, Foreign key (required)</br>
•	Writer – the Song's Writer</br>
•	Price – decimal (required)</br>
•	SongPerformers – a collection of type SongPerformer</br>
Album</br>
•	Id – integer, Primary Key</br>
•	Name – text with max length 40 (required)</br>
•	ReleaseDate – date (required)</br>
•	Price – calculated property (the sum of all song prices in the album)</br>
•	ProducerId – integer, foreign key</br>
•	Producer – the Album's Producer</br>
•	Songs – a collection of all Songs in the Album </br>
Performer</br>
•	Id – integer, Primary Key</br>
•	FirstName – text with max length 20 (required) </br>
•	LastName – text with max length 20 (required) </br>
•	Age – integer (required)</br>
•	NetWorth – decimal (required)</br>
•	PerformerSongs – a collection of type SongPerformer</br>
Producer</br>
•	Id – integer, Primary Key</br>
•	Name – text with max length 30 (required)</br>
•	Pseudonym – text</br>
•	PhoneNumber – text</br>
•	Albums – a collection of type Album</br>
Writer</br>
•	Id – integer, Primary Key</br>
•	Name – text with max length 20 (required)</br>
•	Pseudonym – text</br>
•	Songs – a collection of type Song</br>
SongPerformer</br>
•	SongId – integer, Primary Key</br>
•	Song – the performer's Song (required)</br>
•	PerformerId – integer, Primary Key</br>
•	Performer – the Song's Performer (required)</br>
</br>
Table relations</br>
•	One Song can have many Performers</br>
•	One Permormer can have many Songs</br>
•	One Writer can have many Songs</br>
•	One Album can have many Songs</br>
•	One Producer can have many Albums</br>
NOTE: You will need a constructor, accepting DbContextOptions to test your solution in Judge!</br>

### 2.	All Albums Produced by Given Producer
You need to write method string ExportAlbumsInfo(MusicHubDbContext context, int producerId) in the StartUp class that receives a ProducerId. Export all albums which are produced by the provided ProducerId. For each Album, get the Name, ReleaseDate in format the "MM/dd/yyyy", ProducerName, the Album Songs with each Song Name, Price (formatted to the second digit) and the Song WriterName. Sort the Songs by Song Name (descending) and by Writer (ascending). At the end export the Total Album Price with exactly two digits after the decimal place. Sort the Albums by their Total Price (descending).</br>
Example</br>
Output (producerId = 9)</br>
-AlbumName: Devil's advocate</br>
-ReleaseDate: 07/21/2018</br>
-ProducerName: Evgeni Dimitrov</br>
-Songs:</br>
---#1</br>
---SongName: Numb</br>
---Price: 13.99</br>
---Writer: Kara-lynn Sharpous</br>
---#2</br>
---SongName: Ibuprofen</br>
---Price: 26.50</br>
---Writer: Stanford Daykin</br>
-AlbumPrice: 40.49</br>
…</br>
</br>
### 3.	Songs Above Given Duration
You need to write method string ExportSongsAboveDuration(MusicHubDbContext context, int duration) in the StartUp class that receives Song duration (integer, in seconds). Export the songs which are above the given duration. For each Song, export its Name, Performer Full Name, Writer Name, Album Producer and Duration (in format("c")). Sort the Songs by their Name (ascending), and then by Writer (ascending). If a Song has more than one Performer, export all of them and sort them (ascending). If there are no Performers for a given song, don't print the "---Performer" line at all.</br>
Example</br>
Output (duration = 4)</br>
-Song #1</br>
---SongName: Away</br>
---Writer: Norina Renihan</br>
---Performer: Lula Zuan</br>
---AlbumProducer: Georgi Milkov</br>
---Duration: 00:05:35</br>
-Song #2</br>
---SongName: Bentasil</br>
---Writer: Mik Jonathan</br>
---Performer: Zabrina Amor</br>
---AlbumProducer: Dobromir Slavchev</br>
---Duration: 00:04:03</br>
…</br>

