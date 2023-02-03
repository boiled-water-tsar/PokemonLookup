# Introduction

Welcome to the Pokemon Lookup API, where you can search for your favorite Pokemon and retrieve their descriptions in the rich and elegant language of William Shakespeare. Our API utilizes state-of-the-art language processing techniques to convert the original descriptions into the style of the Bard of Avon, imbuing them with a touch of Renaissance flair. Whether you're a fan of Pikachu or Charizard, our API has got you covered. So come and explore the world of Pokemon, and experience their descriptions like never before.

# Usage

Run using docker by running

```
docker build -t pokelookup .
docker run -it --rm -p 80:80 --name great_lookup pokelookup
```

then navigate to http://localhost/swagger/index.html and enjoy some Pokemon descriptions!