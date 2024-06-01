# NYT Strands-like Game

## Overview

This project is a clone of the NYT Strands game, where players connect letters on a grid to form words based on a given category hint. The game features a spanagram, which is a word that spans from one edge of the grid to the other and represents the puzzle's category. The objective is to find all the words and the spanagram.

## Game Rules and Mechanics

### Objective

Connect letters on a grid to form words that belong to a specific category. The spanagram serves as the category hint. The goal is to find all the words, including the spanagram.

### Game Setup

1. **Grid Layout:** The game is played on a rectangular grid filled with letters.
2. **Category Hint:** The spanagram itself serves as the category hint. For example, if the spanagram is "ANIMALS," all words in the grid will be types of animals.
3. **Letters:** Each cell in the grid contains a single letter.
4. **Spanagram:** One word spans from one edge of the grid to the other and represents the category of the puzzle.

### Basic Rules

1. **Forming Words:** Connect adjacent letters (horizontally, vertically, or diagonally) to form words that fit the category hinted by the spanagram.
2. **Use All Letters:** All letters in the grid must be used to form valid words.
3. **Non-overlapping Words:** Words cannot overlap; each letter can only be part of one word.
4. **Spanagram:** Identify the spanagram that spans from one edge of the grid to the other, representing the puzzle's category.

### Hint System

1. **Incorrect Words:** Players can guess words that may not fit the category.
2. **Hint Unlocking:** For every three incorrect but real words guessed, a hint is unlocked.
3. **Hints Provided:** Each hint highlights all the letters of one word in the grid.

### Point System

1. **Speed:** Points are awarded based on how quickly words are found.
2. **Hints Used:** Fewer hints used result in more points.
3. **Incorrect Words:** Fewer incorrect guesses result in more points.
4. **Consecutive Correct Words:** Finding multiple correct words consecutively grants bonus points.

## Example Puzzle Setup

- **Grid Size:** 7x7
- **Spanagram (Category):** "ANIMALS" (spans from the left edge to the right edge of the grid)
- **Other Words:** "CAT," "DOG," "BEAR," "LION," "FROG," "MOUSE"

## Development Steps

### 1. Design the Grid and UI

- Create a responsive grid layout that adapts to different screen sizes.
- Design intuitive UI elements for selecting and connecting letters, displaying hints, showing found words, and tracking incorrect guesses.

### 2. Implement Game Logic

- Implement the logic for connecting letters diagonally, vertically, or horizontally to form words.
- Ensure that words formed fit the category or are real words for the hint system.
- Detect the spanagram spanning from one edge to the other.

### 3. Develop Hint System

- Track the number of incorrect but real word guesses.
- Unlock and display a hint after every three incorrect guesses by highlighting one word's letters.
- Provide visual feedback for the hinted word, such as changing the letter color or background.

### 4. Set Up Category and Word Database

- Develop a database of categories and associated words, including spanagrams.
- Ensure each puzzle has a unique set of words that fit the category and includes the spanagram.

### 5. Implement User Interaction

- Implement controls for mobile and browser versions to connect letters diagonally, vertically, or horizontally.
- Provide feedback for valid and invalid word formations.
- Include undo options and hint unlocking features to enhance user experience.

### 6. Create Point System

- Implement a timer to track the speed of finding words.
- Track hints used and incorrect guesses.
- Award points for speed, fewer hints used, fewer incorrect guesses, and consecutive correct words.

### 7. Testing and Optimization

- Test the game on various devices to ensure smooth performance and responsiveness.
- Gather user feedback to refine gameplay mechanics and difficulty levels.

## Tools and Technologies

- **Game Engine:** Unity (with WebGL for browser and Android support) or Phaser for browser games.
- **Programming Languages:** C# for Unity, JavaScript for Phaser.
- **Design Tools:** Adobe XD, Figma, or Sketch for UI/UX design.
- **Version Control:** GitHub or GitLab for code management and collaboration.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please read the [CONTRIBUTING](CONTRIBUTING.md) guidelines before submitting a pull request.

## Acknowledgements

- Inspiration from the NYT Strands game.
- OpenAI for assistance in planning the project.

## Contact

For any inquiries, please contact [your-email@example.com].

