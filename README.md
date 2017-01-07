EDIT Code rafactoring in progress

Welcome to my first ASP.NET Application. It's made all by myself.

I created this web application for generating exam dates and hours for university. The biggest problem to prearrange exam date
is collision between exams, in example there is student who has planned exam in Math at the same time as Programming. This collision generate more
work for person who organize this.

To solve this issue I used a concept of graph coloring. I created alghoritm to color every vertex(vertex is represented by exam and graph is collectiong of exams)
and solved problem.