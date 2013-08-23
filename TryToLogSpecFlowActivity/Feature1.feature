Feature: Feature1

Scenario: Some scenario - number 1
	Given Step One
	When Step Two
	Then Step Three

Scenario: Some scenario - number 2
	Given Step One
	When Step Two, But Fail!
	Then Step Three
