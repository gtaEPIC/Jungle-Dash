# Jungle Dash
A COMP305 - 3 Group 3 project

# Protection Rules
You cannot push updates directly to the main branch.<br>
Instead, create a new branch, make those changes, and then create a pull request.<br>
This way **EVERYONE MUST APPROVE OF THE CHANGES** before it goes to the main branch.

[I (Johnathan)](https://github.com/gtaEPIC) can manage making some of the branches and issues,
but if you know how as well that would be great.

Please ask me if you need help with the repository.

# Application suggestions
## [Gitkraken](https://www.gitkraken.com/download)
Strongly recommend, although this app will only work (with the free plan) on public repositories (like this one) it makes managing repositories really easy.

## [Github Desktop](https://desktop.github.com)
This should work, but I don't know how well it will work

# Troubleshooting
## Accidentally made a commit on main and can't push anymore
If you made a commit on your local repository on the main branch, all you will need to do is change the branch it uploads to.

In Gitkraken (github desktop should be similar), right click the branch and select `Set Upstream`, make a branch name (2-3 word summary of the changes you are making), and then push. Now you can make a pull request

If you are using the console, change the branch to push to:
```
git branch --set-upstream-to origin/<NEW BRANCH NAME>
```
Then push your changes
```
git push
```
Now you can make a pull request with the new branch you made


# Group 3 Members
[Johnathan Hall](https://github.com/gtaEPIC)<br>
Evgueni Antsyferov (Waiting for github username)<br>
[Jeremy Liao](https://github.com/jibbooo)