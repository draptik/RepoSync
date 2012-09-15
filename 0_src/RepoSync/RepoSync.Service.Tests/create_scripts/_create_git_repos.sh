#!/bin/bash
#
# Purpose:
#
# Create a folder containing different git repositories which can be
# used for integration testing
#
# Details:
#
# Create 3 git repositories:
#
#   - "bare" a bare git repository
#   - "home" git repo with origin "bare"
#   - "work" git repo with origin "bare"
#
#
# Note to self: We don't need printline debugging, we can call the
# script with
#
#   bash -x _create_git_repos.sh
#

# Command line arguments ---------------------------------
#
# Define the scenario to set up at the end of this script.
#
# "bare": bare repo will be ahead of home repo (default)
# "home": home repo will be ahead of bare repo
scenario=$1
if [ "$scenario" != "bare" -a "$scenario" != "home" ]; then
	scenario="bare" # default
fi


# Variables ----------------------------------------------
GIT=`which git`
today=$(date +"%Y_%m_%d")

# SANDBOX="$HOME/tmp/Z_deleteme_git_repos_$today"
SANDBOX="$HOME/tmp/Z_deleteme_git_repos"


projectname="project1"

homeRepo="$SANDBOX/$projectname"
bareRepo="$homeRepo.git"

work_suffix="_work"
workRepo="$SANDBOX/$projectname$work_suffix"


# Create sandbox if needed; otherwise clean sandbox ------
if [ -d "$SANDBOX" ]; then
	cd "$SANDBOX" && rm -rf *
else
	mkdir "$SANDBOX"
fi

# entering sandbox
cd "$SANDBOX"


# create git repo ----------------------------------------
#
# 1. create project folder
mkdir "$homeRepo"

# 2. create content
echo "some content" > "$homeRepo/readme.txt"
echo "some content" > "$homeRepo/foo.txt"
echo "some content" > "$homeRepo/bar.txt"

# 3. create home repo
cd "$homeRepo"
$GIT init
$GIT add .
$GIT commit -a -m "initial commit"

# clone git repo to bare repo ----------------------------
cd "$SANDBOX"
$GIT clone --bare "$homeRepo" "$bareRepo"

# configure home repo to point to bare repo --------------
cd "$homeRepo"
$GIT remote add origin "$bareRepo"
$GIT config branch.master.remote origin
$GIT config branch.master.merge refs/heads/master

# clone git repo (simulate work) -------------------------
cd "$SANDBOX"
$GIT clone "$bareRepo" "$workRepo" 


# ========================================================
# Scenario 1: bare repo is ahead of home repo
# ========================================================
if [ "$scenario" = "bare" ]; then
    # edit..
	echo "something new" >> "$workRepo/readme.txt"

    # commit & push to bare repo
	cd "$workRepo"
	$GIT commit -a -m"updated readme from work repo"
	$GIT push origin master

    # pull changes from bare repo
    # $GIT pull origin master
fi

# ========================================================
# Scenario 2: home repo is ahead of bare repo
# ========================================================
if [ "$scenario" = "home" ]; then
    # edit..
	echo "something new" >> "$homeRepo/readme.txt"

    # commit
	cd "$homeRepo"
	$GIT commit -a -m"updated readme from home repo"
	#$GIT push origin master
fi
