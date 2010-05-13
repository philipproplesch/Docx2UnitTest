Docx2UnitTest
=============
A Visual Studio 2010 custom tool to generate unit tests from Microsoft(r) 
Word(r) 2007/2010 documents.

Features:
 - It keeps the implementation of tests during re-generation.
 - It keeps test methods attributes

Authors:
 Philip Proplesch <philip@proplesch.de>
 Daniel Fisher <info@lennybacon.com>

Acknowledgements:
 The idea was taken from Thomas Bandt's blog post:
 http://blog.thomasbandt.de/39/2335/word-to-code- kleiner-tdd-helper.html

How-To:
 Just add a *.docx file (like the included sample) to a project in 
 Visual Studio. The test classes will be generated immediately.

 To specify a specific testing framework edit the following line in 
 the document:

	Testing framework: {SupportedTestFramework}

Supported frameworks are:
 - MsTest
 - NUnit
 - XUnit
 - MbUnit
  
Bugs:
 None known yet. Please add tickets to the GitHub project.
 
Wishlist:
 Contact us by e-mail. 