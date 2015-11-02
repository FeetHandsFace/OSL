using System;

public class Merchant {

	public bool stolenFrom;
	public int shortChanged;
	public Inventory inven;
	Secret randomSecret;

	public Merchant(Inventory invntry, Secret rando) {
		randomSecret = rando;
		inven = invntry;
		stolenFrom = false;
		shortChanged = 0;
	}

	public Merchant(Inventory invntry, Secret rando, bool stolen, int shortted) {
		randomSecret = rando;
		inven = invntry;
		stolenFrom = stolen;
		shortChanged = shortted;
	}

    public void beginTrade() {
		inven.initiateTrade(randomSecret, this);
	}
}
