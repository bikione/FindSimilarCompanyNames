# Company Name Comparison Program

This program reads company names from a text file and finds names that might be similar or duplicates.

## How It Works

The program works in three main steps:

### 1. Preparation Step

- **Getting Data Ready**: Each company name is prepared for comparison by "normalizing" it.
  - **Normalization**: Removes all characters that are not letters or numbers and changes all letters to lowercase.
  - **Splitting**: Each name is split into separate words based on non-alphanumeric characters. This gives us a list of words for each name, making it easier to compare them.

### 2. Exact Match Comparison

- **Checking for Exact Matches**: This step looks for exact matches between normalized company names.
  - If two normalized names are exactly the same, we consider them the same company and mark them as duplicates.

### 3. Partial Match Comparison

- **Flexible Comparison**: This step compares the lists of words made in the Preparation Step, so we can find names that are similar even if the word order is different.
  - This helps find names like "University of Chicago" and "Chicago University," which have the same words in a different order.
- **Threshold Matching**: Names are considered similar if most of the words in the shorter list are also in the longer list. This way, names that are mostly the same can be matched, even if the words are not in the exact same order.

---

These steps allow the program to find and mark similar company names.
