if [ $# = 0 ]; then
	echo "not enough arguments";
else
	./ilmerge $1 *.dll -out:build.exe
fi
