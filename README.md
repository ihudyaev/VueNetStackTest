# VueNetStackTest
 Vue js + ASP.net Core + PostgreSQL + Elastic Search Test

@vue/cli 4.3.0
PostgreSQL v12
Elastic Search v7.x
ASP.net Core v3.1

### Steps to Run:
## Postgre
1. Deploy database from ForumDb.sql to PostgreSQL. 
##ASP.net Core Api
2. Open folder ihud.WebApi and run sln. Set port 4000 (used port in client solution) and uncheck use https.
3. Set connection string to your database at appsettings.json
4. Run solution.
## ElasticSearch
5. Run ElasticSearch server on default port. 
6. Check connetion settings at ConnectionToEs.cs file
## Vue Js Client
7. Open client in forumclientapp folder.
8. Run npm install 
9. Check vue cli
10. npm run serve
11. Open http://localhost:8080

## Conection Properties:
Vue Js : /store/api.js
ASP.net Core: 
1. Elastic search - ConnectionToES class
2. Postgre connection string - appsettings

Functions
Realeased:
+ JWT Auth
+ Add Topic
+ Add Topic Replies
+ Save comment after page refresh
+ Refresh Index on button click
+ Watch users profile
+ Update profile
+ Search

ToDo:
+ Add categories
+ Add Role Based authorization
+ Edit/Delete Comments and Topics by owners
+ Comment replies
+ Change to cookie based auth
+ Index autorefresh
+ Token autorenew
+ use https
+ change html templates

