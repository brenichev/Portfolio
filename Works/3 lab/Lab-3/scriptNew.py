import nltk
import random
import requests
import string
import os
import matplotlib.pyplot as plt
import numpy as np
from bs4 import BeautifulSoup
from nltk.classify import NaiveBayesClassifier
from nltk.corpus import movie_reviews
from textblob import TextBlob
from nltk import classify
from nltk import ngrams
from nltk.corpus import stopwords 
from nltk.tokenize import word_tokenize

#nltk.download('NaiveBayesClassifier')
#nltk.download('movie_reviews')
#nltk.download('stopwords')
#nltk.download('punkt')

spec_chars = string.punctuation + '\n\xa0«»\t—…-...]['
classifier = NaiveBayesClassifier


def remove_chars(word, chars):
    return "".join([ch for ch in word if ch not in chars])


def unigram_features(dict_words):
    return dict((word, True) for word in dict_words)


def extract_features(corpus, file_ids, cls, feature_extractor=unigram_features):
    return [(feature_extractor(corpus.words(j)), cls) for j in file_ids]


def Scraping(movie_id):
    title_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/'
    headers = {'User-Agent': 'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.5304.88 Safari/537.36'}
    rating_request = requests.get(title_url, headers = headers)
    title = BeautifulSoup(rating_request.text, 'html.parser')
    movie_rating = title.find('span', class_='sc-7ab21ed2-1 jGRxWM').get_text()

    review_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/reviews?ref_=tt_ov_rt'
    request = requests.get(review_url)
    soup = BeautifulSoup(request.text, 'html.parser')

    movie = soup.find('div', class_='subpage_title_block__right-column')

    name = movie.find('div', class_='parent').find('a').get_text()
    year = movie.find('div', class_='parent').find('span').get_text().replace(' ', '').replace('\n', '')

    review = []

    items_list = soup.find('div', class_='lister').find('div', class_='lister-list')
    review_list = items_list.find_all('div', class_='lister-item-content')
    for myitem in review_list:
        parent = myitem.find('span', class_='rating-other-user-rating')
        if parent is not None:
            review_rating = parent.find('span').get_text()
        else:
            review_rating = 'not rated'
        review_title = myitem.find('a', class_='title').get_text().replace(' ', '').replace('\n', '')
        username = myitem.find('div', class_='display-name-date').find('a').get_text()
        posted_date = myitem.find('span', class_='review-date').get_text()
        review_text = myitem.find('div', class_='content').find('div', class_='text show-more__control').get_text()

        review_item = {'username': username,
                       'rating': review_rating,
                       'review_title': review_title,
                       'posted_date': posted_date,
                       'review': review_text}

        review.append(review_item)

    movie_info = {'name': name, 'year': year, 'url': review_url, 'rating': float(movie_rating), 'reviews': review}
    return movie_info

stopwords_english = stopwords.words('english')
important_words = ['above', 'below', 'off', 'over', 'under', 'more', 'most', 'such', 'no', 'nor', 'not', 'only', 'so', 'than', 'too', 'very', 'just', 'but']

stopwords_english_for_bigrams = set(stopwords_english) - set(important_words)

# clean words, i.e. remove stopwords and punctuation
def clean_words(words, stopwords_english):
    words_clean = []
    for word in words:
        word = word.lower()
        if word not in stopwords_english and word not in string.punctuation:
            words_clean.append(word)    
    return words_clean 

# feature extractor function for unigram
def bag_of_words(words):    
    words_dictionary = dict([word, True] for word in words) 
    return words_dictionary

# feature extractor function for ngrams (bigram)
def bag_of_ngrams(words, n=2):
    words_ng = []
    for item in iter(ngrams(words, n)):
        words_ng.append(item)
    words_dictionary = dict([word, True] for word in words_ng)  
    return words_dictionary

# let's define a new function that extracts all features
# i.e. that extracts both unigram and bigrams features
def bag_of_all_words(words, n=2):
    words_clean = clean_words(words, stopwords_english)
    words_clean_for_bigrams = clean_words(words, stopwords_english_for_bigrams)

    unigram_features = bag_of_words(words_clean)
    bigram_features = bag_of_ngrams(words_clean_for_bigrams)

    all_features = unigram_features.copy()
    all_features.update(bigram_features)

    return all_features

def training():
    pos_reviews = []
    for fileid in movie_reviews.fileids('pos'):
        words = movie_reviews.words(fileid)
        pos_reviews.append(words)

    neg_reviews = []
    for fileid in movie_reviews.fileids('neg'):
        words = movie_reviews.words(fileid)
        neg_reviews.append(words)
        # positive reviews feature set
    pos_reviews_set = []
    for words in pos_reviews:
        pos_reviews_set.append((bag_of_all_words(words), 'pos'))

    # negative reviews feature set
    neg_reviews_set = []
    for words in neg_reviews:
        neg_reviews_set.append((bag_of_all_words(words), 'neg'))
    from random import shuffle 
    shuffle(pos_reviews_set)
    shuffle(neg_reviews_set)

    test_set = pos_reviews_set[:200] + neg_reviews_set[:200]
    train_set = pos_reviews_set[200:] + neg_reviews_set[200:]

    global classifier
    classifier = NaiveBayesClassifier.train(train_set)

    accuracy = classify.accuracy(classifier, test_set)
    print(accuracy) # Output: 0.8025

    # custom_review = "I hated the film. It was a disaster. Poor direction, bad acting."
    # custom_review_tokens = word_tokenize(custom_review)
    # custom_review_set = bag_of_all_words(custom_review_tokens)
    # #print (classifier.classify(custom_review_set)) # Output: neg
    # # Negative review correctly classified as negative

    # # probability result
    # prob_result = classifier.prob_classify(custom_review_set)
    # print (prob_result) # Output: <ProbDist with 2 samples>
    # print (prob_result.max()) # Output: neg
    # print (prob_result.prob("neg")) # Output: 0.770612685688
    # print (prob_result.prob("pos")) # Output: 0.229387314312


    # custom_review = "It was a wonderful and amazing movie. I loved it. Best direction, good acting."
    # custom_review_tokens = word_tokenize(custom_review)
    # custom_review_set = bag_of_all_words(custom_review_tokens)

    # print (classifier.classify(custom_review_set)) # Output: pos
    # # Positive review correctly classified as positive

    # # probability result
    # prob_result = classifier.prob_classify(custom_review_set)
    # print (prob_result) # Output: <ProbDist with 2 samples>
    # print (prob_result.max()) # Output: pos
    # print (prob_result.prob("neg")) # Output: 0.00677736186354
    # print (prob_result.prob("pos")) # Output: 0.993222638136

def analysis(input):    
    custom_review_tokens = word_tokenize(input)
    custom_review_set = bag_of_all_words(custom_review_tokens)
    prob_result = classifier.prob_classify(custom_review_set)
    print(prob_result.prob("pos"))
    print(prob_result.prob("neg"))    
    if prob_result.prob("neg") < prob_result.prob("pos"):
        print(prob_result.prob("pos"))
        return round(prob_result.prob("pos"), 2)
    else:
        print(prob_result.prob("neg"))
        return round(prob_result.prob("neg"), 2) * -1

    # if prob_result.prob("neg") != 0:
    #     return round(prob_result.prob("neg"), 2)
    # else:
    #     return round(prob_result.prob("pos"), 2)


def DoPlot(x_input, y_input, x_label, y_label, title):
    x = np.array(x_input)
    y = np.array(y_input)
    args = np.argsort(x)
    x = x[args]
    y = y[args]
    plt.xlabel(x_label)
    plt.ylabel(y_label)
    plt.title(title)
    plt.scatter(x, y)
    fit = np.polyfit(x, y, deg=4)
    p = np.poly1d(fit)
    plt.plot(x, p(x), "r--")


if __name__ == '__main__':
    training()

    if not os.path.exists('Reviews'):
        os.mkdir('Reviews')

    positive = []
    negative = []
    neutral = []
    rating = []

    pred5 = ['0111161', '0068646', '7375466', '2071491', '1596342']
    rand5 = [random.randint(1, 9999999) for n in range(5)]
    for i in pred5:
        if len(str(i)) < 7:
            list_movie = Scraping('0' * (7 - len(str(i))) + str(i))
        else:
            list_movie = Scraping(i)

        count = 0
        print(f"{list_movie['url']}\n{list_movie['name']} {list_movie['year']} {list_movie['rating']}/10")
        with open(f"Reviews/{list_movie['name'].replace(':', '')} (reviews).txt", 'w+', encoding="utf-8") as file:
            file.write(f"{list_movie['url']}\n{list_movie['name']} {list_movie['year']} {list_movie['rating']}/10\n")
            for item in list_movie['reviews']:
                count += 1
                vis_review = item['review'].replace('. ', '.\n||')
                print(".................................................")
                print(f"{item['username']} {item['posted_date']}")
                print(f"{item['review_title']} ({item['rating']}/10)")
                if item['rating'] == 'not rated':
                    rating.append(int(0))
                else:
                    rating.append(int(item['rating']))
                print(f"||{vis_review}")

                analyze = analysis(item['review'])

                file.write(".................................................\n" +
                           f"{item['username']} {item['posted_date']}\n" +
                           f"{item['review_title']} ({item['rating']}/10)\n" +
                           f"||{vis_review}\n" +
                           f"Тональность текста: ({analyze}/1)\n")
                if analyze < 0:
                    negative.append(analyze)
                    positive.append(0.0)
                elif analyze > 0:
                    negative.append(0.0)
                    positive.append(analyze)

            DoPlot(positive, rating, "Тональность", "Рейтинг", "Зависимость рейтинга от тональности")
            try:
                DoPlot(negative, rating, "Тональность", "Рейтинг", "Зависимость рейтинга от тональности")
            except Exception as e:
                print(e)

            plt.savefig(f"Reviews/{list_movie['name'].replace(':', '')} (plot).png", bbox_inches='tight')
            #plt.show()
            
