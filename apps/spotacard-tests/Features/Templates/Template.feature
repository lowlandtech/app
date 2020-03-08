Feature: Templates
  As a user I want to manage templates

@TestCase1
Scenario: Create new template
  Given a title
  And content
  And tags are added
  When the template is saved
  Then a template card should have been created
  And has a slug
  And has tags
  And a content card should have been created
  And a edge should have been created
  And the edge should have index 1
  And the edge should have template as a parent
  And the edge should have content as a child
  And the edge has a label of VERSION
  And the content card has a tag of 0.0.0
  And the content card has a tag of latest
