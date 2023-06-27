#!/bin/bash
# Made by Leonardo Comerci | @leonardocomerci

CURRENT_FOLDER=$(pwd)
SLEEP_TIME=5

echo "Levantando cobro desde: $CURRENT_FOLDER"
sleep $SLEEP_TIME
echo "Se invocó: $CURRENT_FOLDER/fake-cobro.sh"
echo "Tardó $SLEEP_TIME segundos en ejecutarse."
 
